using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libALAlertBannerSDK.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true)]
