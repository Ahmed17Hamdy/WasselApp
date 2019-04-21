using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TK.CustomMap.Droid;
using WasselApp.CustomControls;
using WasselApp.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Map), typeof(MyMapRenderer))]
namespace WasselApp.Droid.Renderers
{
    public class MyMapRenderer : TKCustomMapRenderer, Android.Gms.Maps.GoogleMap.IInfoWindowAdapter
    {
        public MyMapRenderer(Context context) : base(context)
        {
        }

        public override void OnMapReady(Android.Gms.Maps.GoogleMap googleMap)
        {
            base.OnMapReady(googleMap);

            googleMap.SetInfoWindowAdapter(this);
        }



        Android.Views.View Android.Gms.Maps.GoogleMap.IInfoWindowAdapter.GetInfoContents(Android.Gms.Maps.Model.Marker marker)
        {
            var pin = GetPinByMarker(marker);
            if (pin == null) return null;

            var image = new ImageView(Context);
            image.SetImageResource(Resource.Drawable.van);

            return image;
        }

        Android.Views.View Android.Gms.Maps.GoogleMap.IInfoWindowAdapter.GetInfoWindow(Android.Gms.Maps.Model.Marker marker)
        {
            return null;
        }
    }
}