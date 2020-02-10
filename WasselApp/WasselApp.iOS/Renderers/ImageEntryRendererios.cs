using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using WasselApp.CustomControls;
using WasselApp.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRendererios))]
namespace WasselApp.iOS.Renderers
{
    public class ImageEntryRendererios : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (ImageEntry)this.Element;
            var textField = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        textField.LeftViewMode = UITextFieldViewMode.Always;
                        textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                    case ImageAlignment.Right:
                        textField.RightViewMode = UITextFieldViewMode.Always;
                        textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                }
            }

            textField.BorderStyle = UITextBorderStyle.None;
            CALayer bottomBorder = new CALayer
            {
                Frame = new CGRect(0.0f, element.HeightRequest - 1, this.Frame.Width, 1.0f),
                BorderWidth = 2.0f,
                BorderColor = element.LineColor.ToCGColor()
            };
            Control.Layer.CornerRadius = 20;
            Control.Layer.BorderWidth = 0.5f;
            Control.Layer.BorderColor = Xamarin.Forms.Color.Blue.ToCGColor();
            //Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));

            //Control.LeftViewMode = UITextFieldViewMode.Always;
            textField.Layer.AddSublayer(bottomBorder);
            textField.Layer.MasksToBounds = true;

        }

        public UIView GetImageView(string image, int height, int width)
        {

            var uiImageView = new UIImageView(UIImage.FromBundle(image));
            {
                Frame = new RectangleF(0, 0, width - 10, height - 10)
            ;
            };
            UIView objLeftView = new UIView(frame: new System.Drawing.Rectangle(0, 0, width - 5, height - 5));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }
    }
}