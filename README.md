# XamarinChangeBundleId
XamarinChangeBundleId is a tool which helps to change BundleId/PackageName for Xamarin projects via a continious deployment tool such as TeamCity, Jenkins and similar tools. It will preserve all other values and only change the BundleId value.

For different staging builds you might need to update the bundleId of your Xamarin app, example:
- com.mydomain.ext.myapp.test for an application that runs in a test environment
- com.mydomain.ext.myapp for the same application that runs in a production environment
- com.mydomain.int.myapp for the same application that runs in an inhouse environment

Making these changes manually is hard, boring and error prone, this tool will help you to automate these steps.

Language: c#, Console application

# Syntax
This tool takes three parameters:
1. platform; Valid values: ios or droid
2. bundleid; the bundleid (iOS) or packagename (Android) you would like to set
3. Relative path to the info.plist or AndroidManifest.xml file

# Sample usage with TeamCity
Before the build step of your iOS project add another build step named "Modify info.plist".
The type shall be "Run: Custom script".

In the Custom script content, enter the following:
PathToExe\XamarinChangeBundleId.exe ios com.mydomain.myappname RelativePathToThePlistFile

Before the build step of your Android project add another build step named "Modify manifest".
The type shall be "Run: Custom script".

In the Custom script content, enter the following:
PathToExe\XamarinChangeBundleId.exe droid com.mydomain.myappname RelativePathToTheManifestFile

# License
Totally free!





