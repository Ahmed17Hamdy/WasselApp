using Android.Views;
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Support.V4.Widget;
using WasselApp.Helpers;
using Xamarin.Forms;

namespace WasselApp.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class MasterDetailsRenderer : MasterDetailPageRenderer
    {
        protected override void OnElementChanged(VisualElement oldElement, VisualElement newElement)
        {
            base.OnElementChanged(oldElement, newElement);

            var fieldInfo = GetType().BaseType.GetField("_masterLayout", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var _masterLayout = (ViewGroup)fieldInfo.GetValue(this);
            var lp = new DrawerLayout.LayoutParams(_masterLayout.LayoutParameters);
            lp.Gravity = GravityClass.Grav();
            _masterLayout.LayoutParameters = lp;
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}