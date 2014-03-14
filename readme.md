ALALertBanner iOS Binding
=========

This is a Xamarin iOS binding for the [ALAlertBanner] native library.

## Everyday usage

Basic code to create a basic alert banner:

    using MonoTouch.ALAlertBanner;

    var banner = ALAlertBanner.ALAlertBanner.AlertBannerForView(...);
    banner.Show();

## Native code source

Use the `xamarin` branch of [this fork] of ALAlertBanner from GitHub.

## Updating the binding: no interface changes

When the ALAlertBanner library is updated but there are no changes to its public header files (in other words only the implementation code has changed), then you just need to rebuild and update the static library file included in this binding project.

Go to your working copy of the native ALAlertBanner git repository and find the directory containing `Makefile`. Run `make clean && make all`. This must be done on a Mac OS X machine with the Xcode command line tools install. See the “Requirements” and “Creating a static library” sections of the [Binding Objective-C Walkthrough][walkthrough] document.

The `make` command will create a new `libALAlertBannerSDK.a` file in the current directory. It’s a “fat binary” static library containing native code for both Intel (iOS Simulator) and ARM (iOS device) platforms. Copy this file to the MonoTouch.ALAlertBanner project directory.

## Updating the binding: interface changes

When there are changes to the public header files of the ALAlertBanner library, additional steps are needed to update the generated C# code in the bindings project. These steps are based on the [Binding Objective-C Walkthrough][walkthrough] document.

“Public header files” refers to the following two files: ALAlertBanner.h and ALAlertBannerManager.h.

1. Update `libALAlertBannerSDK.a` according to the instructions in the previous section
2. Install Objective Sharpie if you have not already (see the Requirements section of the walkthrough)
3. Run Objective Sharpie
    - Select iOS 7.0 as the Target SDK
    - Tap the `[+]` button under *Select Header Files* and choose the following two files: ALAlertBanner.h and ALAlertBannerManager.h
    - Enter `MonoTouch.ALAlertBanner` for the C# Namespace
    - Tap the Generate button
    - Choose a location to save the generated file. Either overwrite `ApiDefinition.cs` in this bindings project, or save to a temporary location and use a diff tool to merge the changes
4. Clean up the generated code: see below
5. Build and test using the BindingsTest project, and commit changes

### Cleaning up generated code

Due to limitations of the tool, the code generated by Objective Sharpie needs some manual cleanup before it’s usable. That is why codegen is a manual step done occasionally and the generated code files are kept in source control.

Use the existing ApiDefinition.cs file as a guide, and the [Binding Objective-C Walkthrough][walkthrough] for instructions on basic cleanup tasks.

Ensure that the following is done:

- Move `enum` declarations to `StructsAndEnums.cs`, cleaning up the bad syntax generated by the tool
- Correct `using` statements (e.g. MonoTouch.ObjCRuntime, MonoTouch.Foundation, and MonoTouch.UIKit) are required
- Comment out the overload of `AlertBannerForView` that takes a `Delegate` object (or figure out how to make it compile)
- Remove `Verify` attributes. We are currently keeping array properties declared as `NSObject []`


[ALALertBanner]: https://github.com/alobi/ALAlertBanner
[this fork]: https://github.com/mmertsock/ALAlertBanner
[walkthrough]: http://docs.xamarin.com/guides/ios/advanced_topics/binding_objective-c/Walkthrough_Binding_objective-c_library/
