using System;
using System.Threading.Tasks;

namespace Orchard
{
    public interface IMediaPicker
    {
        Task<string> PickPhoto();
    }
}

