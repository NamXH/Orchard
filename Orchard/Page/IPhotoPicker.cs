using System;
using System.Threading.Tasks;
using System.IO;

namespace Orchard
{
    public interface IMediaPicker
    {
        Task<Stream> PickPhoto();
        Task<Stream> TakePhoto();
    }
}

