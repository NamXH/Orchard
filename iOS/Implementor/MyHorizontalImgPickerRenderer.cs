using System;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Orchard;
using Orchard.iOS;
using MonoTouch.CoreGraphics;
using System.Drawing;


[assembly: ExportRenderer(typeof(MyHorizontalImgPicker), typeof(MyHorizontalImgPickerRenderer))]
namespace Orchard.iOS
{
    public class MyHorizontalImgPickerRenderer : ViewRenderer<MyHorizontalImgPicker, UIView>
    {
        UIScrollView _scrollView;

        const int PageControlHeight = 20;

        protected override void OnElementChanged(ElementChangedEventArgs<MyHorizontalImgPicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var view = new UIView(new System.Drawing.RectangleF(0, 0, (float)e.NewElement.WidthRequest, (float)e.NewElement.HeightRequest));
                // Initial setups.
                _scrollView = new UIScrollView(new System.Drawing.RectangleF(0, 0, (float)e.NewElement.WidthRequest, (float)e.NewElement.HeightRequest - PageControlHeight));

                var pageControl = new UIPageControl(new System.Drawing.RectangleF(0, (float)e.NewElement.HeightRequest - PageControlHeight, (float)e.NewElement.WidthRequest, PageControlHeight));
                pageControl.Pages = e.NewElement.Children.Count;
                pageControl.AutoresizingMask = UIViewAutoresizing.All;
                pageControl.BackgroundColor = UIColor.Brown;
                pageControl.Enabled = false;

                view.Add(_scrollView);
                view.Add(pageControl);
                view.BringSubviewToFront(_scrollView);

                SetNativeControl(view);

                _scrollView.PagingEnabled = true;
                _scrollView.ShowsHorizontalScrollIndicator = false;
                _scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
           

                _scrollView.BackgroundColor = UIColor.LightGray;

                foreach (var subView in e.NewElement.Children)
                {
                    subView.HeightRequest = e.NewElement.HeightRequest - PageControlHeight;
                    subView.VerticalOptions = LayoutOptions.Start;
                    var renderer = RendererFactory.GetRenderer(subView);
                    _scrollView.Add(renderer.NativeView);
                }

                _scrollView.Scrolled += (object sender, EventArgs earg) =>
                {
                    if (_scrollView.Dragging && _scrollView.Decelerating)
                    {
                        pageControl.CurrentPage = (int)(Math.Round(_scrollView.ContentOffset.X / (_scrollView.ContentSize.Width / pageControl.Pages)));
                        e.NewElement.SelectedIndex = pageControl.CurrentPage;
                    }
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName)
            {
                var currSize = Element.ContentSize.ToSizeF();
                currSize.Height -= PageControlHeight;
                _scrollView.ContentSize = currSize;
            }
        }
    }
}

