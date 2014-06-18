using MonoTouch.UIKit;
using System;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XForms.Toolkit.Controls;
using XForms.Toolkit.iOS.Controls.ImageButton;

[assembly: ExportRenderer(typeof(ImageButton), typeof(ImageButtonRenderer))]
namespace XForms.Toolkit.iOS.Controls.ImageButton
{
    public class ImageButtonRenderer : ButtonRenderer
    {
        private const int controlPadding = 2;

        private  Toolkit.Controls.ImageButton ImageButton { get { return (XForms.Toolkit.Controls.ImageButton)Element; } }

        private const string iPad = "iPad";
        private const string iPhone = "iPhone";

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var imageButton = this.ImageButton;
            var targetButton = Control as UIButton;
            if (imageButton != null && targetButton != null && !String.IsNullOrEmpty(imageButton.Image))
            {
                SetImage(imageButton.Image, imageButton.ImageWidthRequest, imageButton.ImageHeightRequest, targetButton);
                   
            }
        }

        private void SetImage(string imageName, int widthRequest, int heightRequest, UIButton targetButton)
        {
            var image = UIImage.LoadFromData(MonoTouch.Foundation.NSData.FromUrl(new Uri("http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/images/Images/Local-sml.png")));

            targetButton.SetBackgroundImage(image, UIControlState.Normal);

            switch (ImageButton.Orientation)
            {
                case TextAligment.Left:
                    {
                        targetButton.TitleEdgeInsets = new UIEdgeInsets(0, 20, 0, 0);
                        targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                    }
                    break;
                case TextAligment.Top:
                    {
                        targetButton.TitleEdgeInsets = new UIEdgeInsets(5, 0, 0, 0);
                        targetButton.VerticalAlignment = UIControlContentVerticalAlignment.Top;
                    }
                    break;
                case TextAligment.Right:
                    {
                        targetButton.TitleEdgeInsets = new UIEdgeInsets(0, 0, 0, 20);
                        targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
                    }
                    break;
                case TextAligment.Bottom:
                    {
                        targetButton.TitleEdgeInsets = new UIEdgeInsets(0, 0, 5, 0);
                        targetButton.VerticalAlignment = UIControlContentVerticalAlignment.Bottom;
                    }
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}