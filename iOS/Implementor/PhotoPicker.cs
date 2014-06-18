using System;
using MonoTouch.UIKit;
using System.Threading;
using System.Linq;
using Xamarin.Forms;
using Orchard.iOS;
using Xamarin.Media;

[assembly: Dependency(typeof(PhotoPicker))]
namespace Orchard.iOS
{
    public class PhotoPicker : IPhotoPicker
    {
        public void Show()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            if (window == null)
                throw new InvalidOperationException("There's no current active window");

            var viewController = window.RootViewController;

            if (viewController == null)
            {
                window = UIApplication.SharedApplication.Windows.OrderByDescending(w => w.WindowLevel).FirstOrDefault(w => w.RootViewController != null);
                if (window == null)
                    throw new InvalidOperationException("Could not find current view controller");
                else
                    viewController = window.RootViewController; 
            }
//
//            while (viewController.PresentedViewController != null)
//                viewController = viewController.PresentedViewController;
//
//            var picker = new UIImagePickerController();
//
//
//            viewController.PresentViewController(picker, true, null);
            var picker = new MediaPicker();
            var controller = picker.GetPickPhotoUI();
            viewController.PresentViewController(controller, true, null);
        }
    }
}

