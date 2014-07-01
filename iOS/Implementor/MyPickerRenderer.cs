using System;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Orchard;
using Orchard.iOS;
using MonoTouch.CoreGraphics;
using System.Drawing;


[assembly: ExportRenderer(typeof(MyPicker), typeof(MyPickerRenderer))]
namespace Orchard.iOS
{
    public class MyPickerRenderer : ViewRenderer<MyPicker, UIScrollView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MyPicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                // Initial setups.
                var pickerView = new UIScrollView(new System.Drawing.RectangleF(0, 0, (float)e.NewElement.WidthRequest, (float)e.NewElement.HeightRequest));

                SetNativeControl(pickerView);

                Control.PagingEnabled = true;
                Control.ShowsHorizontalScrollIndicator = false;
                Control.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
           

                Control.BackgroundColor = UIColor.LightGray;

                foreach (var view in e.NewElement.Children)
                {
                    var renderer = RendererFactory.GetRenderer(view);
                    Control.Add(renderer.NativeView);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName)
            {
                Control.ContentSize = Element.ContentSize.ToSizeF();
            }
        }
    }

}

