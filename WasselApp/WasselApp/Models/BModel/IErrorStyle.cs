using Xamarin.Forms;

namespace WasselApp.Models.BModel
{
    public interface IErrorStyle
    {
        void ShowError(View view, string message);
        void RemoveError(View view);
    }
}
