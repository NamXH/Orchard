using System;
using Xamarin.Forms;
using Orchard.iOS;
using System.Threading.Tasks;
using Xamarin.Media;
using System.Diagnostics;
using MonoTouch.UIKit;
using System.Linq;
using MonoTouch.CoreGraphics;

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
            //picker.PrefersStatusBarHidden();
            picker.ToolbarHidden = true;

            var screenSize = UIScreen.MainScreen.Bounds.Size;
            var cameraAspectRatio = 4.0 / 3.0;
            var imgHeight = Math.Floor(screenSize.Width * cameraAspectRatio);
            var scaleFactor = (float)Math.Ceiling(screenSize.Height / imgHeight);

            picker.CameraViewTransform = CGAffineTransform.MakeScale(scaleFactor, scaleFactor);
            //picker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);

            //var overlayRect = CGRectMake(0, 0, self.imagePickerController.view.frame.size.width,      self.imagePickerController.view.frame.size.height);

            var overlayView = new UIView(new System.Drawing.RectangleF(0, 0, picker.View.Frame.Width, picker.View.Frame.Height));

            var unitSlider = new UISlider(new System.Drawing.RectangleF(-10, picker.View.Frame.Height - 50, 150, 30));
            unitSlider.Layer.AnchorPoint = new System.Drawing.PointF(0, 0);
            unitSlider.Transform = CGAffineTransform.MakeRotation((float)(-Math.PI / 2));

            overlayView.AddSubview(unitSlider);
            picker.CameraOverlayView = overlayView;

            uIViewController.PresentViewController(picker, true, null);
            return (double)0;
        }

    }
}

