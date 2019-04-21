using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using WasselApp.iOS.Effects;
using Xamarin.Forms;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(iOSBackgroundEntryEffect), "BackgroundEffect")]
namespace WasselApp.iOS.Effects
{
    public class iOSBackgroundEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var view = this.Control as UITextField;
                if (view == null)
                    return;

                view.BorderStyle = UITextBorderStyle.Line;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {

        }
    }
}