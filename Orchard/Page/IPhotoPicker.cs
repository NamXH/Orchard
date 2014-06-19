using System;
using System.Threading.Tasks;

namespace Orchard
{
    public interface IPhotoPicker
    {
        Task<string> Show();
    }
}

