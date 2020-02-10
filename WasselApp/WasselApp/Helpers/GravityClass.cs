using Android.Views;

namespace WasselApp.Helpers
{
    public class GravityClass
    {
        public static int Grav()
        {
            if (Settings.LastUserGravity == "Arabic")
            {
                return (int)GravityFlags.Right;
            }
            else
            {
                return (int)GravityFlags.Left;
            }
        }
    }
}
