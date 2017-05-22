using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace XamarinChangeBundleId
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Syntax: XamarinChangeBundleId <platform> <bundleId> <pathToPlistOrManifest>");
                Console.WriteLine("  platform: ios or droid");
                Console.WriteLine("  bundleId: The bundleId (ios) or package name (android)");
                Console.WriteLine("  pathToPlistOrManifest: The path to the .plist/.manifest file, relative to this tools folder");
                return;
            }
            var platform = args[0].ToLowerInvariant();
            var bundleId = args[1];
            var pathToFile = args[2];

            var filename = Path.Combine(Directory.GetCurrentDirectory(), pathToFile);
            var doc = XDocument.Load(filename);
            if (platform.Equals("ios"))
            {
                if (doc.Root != null)
                {
                    var qry = from el in doc.Root.Element("dict").Elements("key")
                              where el.Value == "CFBundleIdentifier"
                              select el.NextNode;

                    var element = qry.FirstOrDefault();
                    if (element != null)
                    {
                        ((XElement)element).Value = bundleId;
                    }

                    var version = string.Empty;
                    var verQry = from el in doc.Root.Element("dict").Elements("key")
                        where el.Value == "CFBundleShortVersionString"
                        select el.NextNode;
                    element = verQry.FirstOrDefault();
                    if (element != null)
                    {
                        version = ((XElement) element).Value;
                    }
                    if (!string.IsNullOrEmpty(version))
                    {
                        qry = from el in doc.Root.Element("dict").Elements("key")
                            where el.Value == "CFBundleVersion"
                              select el.NextNode;

                        element = qry.FirstOrDefault();
                        if (element != null)
                        {
                            ((XElement)element).Value = version;
                        }
                    }

                    File.WriteAllText(filename, doc.ToString());
                    Console.WriteLine($"Updated BundleId to {bundleId} for platform {platform}");
                }
            }
            else if (platform.Equals("droid"))
            {
                if (doc.Root != null)
                {
                    var attribute = doc.Root.Attribute("package");
                    if (attribute != null)
                    {
                        attribute.Value = bundleId;
                    }
                    File.WriteAllText(filename, doc.ToString());
                    Console.WriteLine($"Updated PackageName to {bundleId} for platform {platform}");
                }
            }
        }
    }
}
