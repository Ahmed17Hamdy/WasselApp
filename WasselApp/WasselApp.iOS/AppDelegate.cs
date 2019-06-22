using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace WasselApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            OneSignal.Current.StartInit("f5f4f650-3453-456c-8024-010ea68e738b")
                .EndInit();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        [Export("oneSignalApplicationDidBecomeActive:")]
        public void OneSignalApplicationDidBecomeActive(UIApplication application)
        {
            // Remove line if you don't have a OnActivated method.
            //   OnActivated(application);
        }

        [Export("oneSignalApplicationWillResignActive:")]
        public void OneSignalApplicationWillResignActive(UIApplication application)
        {
            // Remove line if you don't have a OnResignActivation method.
            //    OnResignActivation(application);
        }

        [Export("oneSignalApplicationDidEnterBackground:")]
        public void OneSignalApplicationDidEnterBackground(UIApplication application)
        {
            // Remove line if you don't have a DidEnterBackground method.
            //   DidEnterBackground(application);
        }

        [Export("oneSignalApplicationWillTerminate:")]
        public void OneSignalApplicationWillTerminate(UIApplication application)
        {
            // Remove line if you don't have a WillTerminate method.
            //  WillTerminate(application);
        }
    }
}
