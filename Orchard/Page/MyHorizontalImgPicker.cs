using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace Orchard
{
    public class MyHorizontalImgPicker : Layout<View>
    {
        public MyHorizontalImgPicker()
        {
            IsClippedToBounds = true;
        }

        public static readonly BindableProperty ContentSizeProperty = BindableProperty.Create<MyHorizontalImgPicker, Size>(mp => mp.ContentSize, default(Size));

        public Size ContentSize
        {
            get
            {
                return (Size)GetValue(ContentSizeProperty);
            }
            private set
            {
                base.SetValue(ContentSizeProperty, value);
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var i = 0;
            foreach (var c in Children)
            {
                var region = new Rectangle(i * width + x, y, width, height);
                ++i;
                Xamarin.Forms.Layout.LayoutChildIntoBoundingRegion(c, region);
            }

            ContentSize = new Size(i * width, height);

            Debug.WriteLine("lc: {0} {1} {2} {3}", x, y, width, height);
        }
    }
}

