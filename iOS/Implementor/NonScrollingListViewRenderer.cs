using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Orchard.iOS;
using Orchard;

[assembly: ExportRenderer(typeof(NonScrollingListView), typeof(NonScrollingListViewRenderer))]
namespace Orchard.iOS
{
    public class NonScrollingListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            Control.ScrollEnabled = false;
        }
    }
}

