using System;
using Xamarin.Media;
using Android.App;
using Orchard.Android;
using Xamarin.Forms;
using Android.Content;
using System.Threading.Tasks;

[assembly: Dependency(typeof(MyMediaPicker))]
namespace Orchard.Android
{
    public class MyMediaPicker : IMediaPicker
    {
        public async Task<string> PickPhoto()
        {
            var picker = new MediaPicker(Application.Context);
            var intent = picker.GetPickPhotoUI();

            intent.SetFlags(ActivityFlags.NewTask);

            Application.Context.StartActivity(intent);
           

            return "to do later";
        }
    }
}

