using System;
using Xamarin.Media;
using Android.App;
using Orchard.Android;
using Xamarin.Forms;
using Android.Content;
using System.Threading.Tasks;
using System.IO;

[assembly: Dependency(typeof(MyMediaPicker))]
namespace Orchard.Android
{
    public class MyMediaPicker : IMediaPicker
    {
        public async Task<Stream> PickPhoto()
        {
            var picker = new MediaPicker(Application.Context);
            var intent = picker.GetPickPhotoUI();

            intent.SetFlags(ActivityFlags.NewTask);

            Application.Context.StartActivity(intent);
           

            return null;
        }

        public async Task<Stream> TakePhoto()
        {
            var picker = new MediaPicker(Application.Context);
            var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions());

            intent.SetFlags(ActivityFlags.NewTask);

            Application.Context.StartActivity(intent);


            return null;
        }
    }
}

