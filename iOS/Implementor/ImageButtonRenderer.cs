using MonoTouch.UIKit;
using System;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Orchard;
using Orchard.iOS;
using System.Reflection;

[assembly: ExportRenderer(typeof(ImageButton), typeof(ImageButtonRenderer))]
namespace Orchard.iOS
{
    public class ImageButtonRenderer : ButtonRenderer
    {
        private const int controlPadding = 2;

        private  ImageButton ImageButton { get { return (ImageButton)Element; } }

        private const string iPad = "iPad";
        private const string iPhone = "iPhone";

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var imageButton = this.ImageButton;

            var targetButton = Control;

            targetButton.Layer.BorderWidth = 1;
            targetButton.Layer.BorderColor = targetButton.CurrentTitleColor.CGColor;

            if (imageButton != null && targetButton != null && !String.IsNullOrEmpty(imageButton.Image))
            {
                SetImage(imageButton.Image, imageButton.ImageWidthRequest, imageButton.ImageHeightRequest, targetButton);
            }
        }

        private void SetImage(string imageName, int widthRequest, int heightRequest, UIButton targetButton)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var image = UIImage.FromResource(assembly, imageName);

            var imageView = new UIImageView();

            var widthCt = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, widthRequest);
            var heightCt = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, heightRequest);

            imageView.AddConstraint(widthCt);
            imageView.AddConstraint(heightCt);
            imageView.TranslatesAutoresizingMaskIntoConstraints = false;
            imageView.Image = image;
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            targetButton.AddSubview(imageView);

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
                        var yCenterCt = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, targetButton, NSLayoutAttribute.CenterY, 1, 0);
                        var xLeftCt = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, targetButton, NSLayoutAttribute.Left, 1, 0);

                        targetButton.AddConstraint(yCenterCt);
                        targetButton.AddConstraint(xLeftCt);

                        targetButton.TitleEdgeInsets = new UIEdgeInsets(0, 0, 0, 20);
                        targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
                    }
                    break;
                case TextAligment.Bottom:
                    {
                        var xCenterCt = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, targetButton, NSLayoutAttribute.CenterX, 1, 0);
                        var yTopCt = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, targetButton, NSLayoutAttribute.Top, 1, 0);

                        targetButton.AddConstraint(xCenterCt);
                        targetButton.AddConstraint(yTopCt);

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