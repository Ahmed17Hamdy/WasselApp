using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TK.CustomMap.Droid;
using Plugin.CurrentActivity;

namespace WasselApp.Droid
{
    [Activity(Label = "WasselApp", Icon = "@mipmap/ic_launcher", Theme = "@style/SplashTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] permission =
           {
            Android.Manifest.Permission.AccessCheckinProperties,
            Android.Manifest.Permission.AccessFineLocation,
            Android.Manifest.Permission.AccessCoarseLocation,
            Android.Manifest.Permission.AccessMockLocation,
            Android.Manifest.Permission.AccessWifiState,
            
            Android.Manifest.Permission.WriteExternalStorage,
            Android.Manifest.Permission.ReadExternalStorage,
            Android.Manifest.Permission.Internet
            };
        const int RequestId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
        
            base.SetTheme(Resource.Style.MainTheme);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            ChechSdk();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            TKGoogleMaps.Init(this, savedInstanceState);
           // CrossCurrentActivity.Current.Activity(this, savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public void ChechSdk()
        {
            if ((int)Build.VERSION.SdkInt > 23)
            {
                RequestPermissions(permission, RequestId);
                return;
            }
            else
            {
                return;
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permission, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}