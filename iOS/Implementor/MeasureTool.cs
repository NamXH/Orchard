using System;
using Xamarin.Forms;
using Orchard.iOS;
using System.Threading.Tasks;
using Xamarin.Media;
using System.Diagnostics;
using MonoTouch.UIKit;
using System.Linq;

[assembly: Dependency(typeof(MeasureTool))]
namespace Orchard.iOS
{
    public class MeasureTool : IMeasureTool
    {

        public async Task<double> DoMeasure()
        {
            UIWindow uIWindow = UIApplication.SharedApplication.KeyWindow;
            if (uIWindow == null)
            {
                throw new InvalidOperationException("There's no current active window");
            }
            UIViewController uIViewController = uIWindow.RootViewController;
            if (uIViewController == null)
            {
                uIWindow = (from w in UIApplication.SharedApplication.Windows
                    orderby w.WindowLevel descending
                                        select w).FirstOrDefault((UIWindow w) => w.RootViewController != null);
                if (uIWindow == null)
                {
                    throw new InvalidOperationException("Could not find current view controller");
                }
                uIViewController = uIWindow.RootViewController;
            }
            while (uIViewController.PresentedViewController != null)
            {
                uIViewController = uIViewController.PresentedViewController;
            }

            var picker = new UIImagePickerController();
            picker.SourceType = UIImagePickerControllerSourceType.Camera;
            picker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Photo;
            picker.CameraDevice = UIImagePickerControllerCameraDevice.Rear;
            picker.ShowsCameraControls = false;
            picker.NavigationBarHidden = true;
            picker.ToolbarHidden = true;



            //picker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);

            uIViewController.PresentViewController(picker, true, null);
            return (double)0;
        }

    }
}

