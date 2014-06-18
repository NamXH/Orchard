using System;
using Xamarin.Media;
using Android.App;
using Orchard.Android;
using Xamarin.Forms;
using Android.Content;

[assembly: Dependency(typeof(PhotoPicker))]
namespace Orchard.Android
{
    public class PhotoPicker : IPhotoPicker
    {

        public void Show()
        {
            var picker = new MediaPicker(Application.Context);
            var intent = picker.GetPickPhotoUI();

            intent.SetFlags(ActivityFlags.NewTask);

            Application.Context.StartActivity(intent);
            //StartActivityForResult(intent, 1);
        }
    }
}

