using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WasselApp.Helpers;
using System.Threading.Tasks;
using WasselApp.Views.DriverPages.DriverOrders;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Com.OneSignal;
using Plugin.Multilingual;

namespace WasselApp.Views.CarsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage ()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                  : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;

            OneSignal.Current.StartInit("f5f4f650-3453-456c-8024-010ea68e738b")
             .InFocusDisplaying(OSInFocusDisplayOption.None)
             .HandleNotificationReceived(OnNotificationRecevied)
             .HandleNotificationOpened(OnNotificationOpened)
             .EndInit();
        }
        private void OnNotificationOpened(OSNotificationOpenedResult result)
        {
            if (result.notification?.payload?.additionalData == null)
            {
                return;
            }

            if (result.notification.payload.additionalData.ContainsKey("body"))
            {
                var labelText = result.notification.payload.additionalData["body"].ToString();
                Settings.LastNotify = labelText;
                App.Current.MainPage = new OrderPushPage();
            }

        }

        private void OnNotificationRecevied(OSNotification notification)
        {
            if (notification.payload?.additionalData == null)
            {
                return;
            }

            if (notification.payload.additionalData.ContainsKey("body"))
            {
                var labelText = notification.payload.additionalData["body"].ToString();
                Settings.LastNotify = labelText;
                App.Current.MainPage = new OrderPushPage();
            }
        }
    }
}