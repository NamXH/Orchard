using System;
using MonoTouch.UIKit;
using System.Threading;
using System.Linq;
using Xamarin.Forms;
using Orchard.iOS;
using Xamarin.Media;
using System.Threading.Tasks;
using System.Diagnostics;

[assembly: Dependency(typeof(PhotoPicker))]
namespace Orchard.iOS
{
    public class PhotoPicker : IPhotoPicker
    {
        public async Task<string> Show()
        {
            var picker = new MediaPicker();
            MediaFile mf = null;
            try
            {
                mf = await picker.PickPhotoAsync();
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("photo picker canceled");
            }
            if (mf == null)
            {
                return null;
            }
            return mf.Path;
        }
    }
}

