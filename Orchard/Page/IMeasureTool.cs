using System;
using System.Threading.Tasks;

namespace Orchard
{
    public interface IMeasureTool
    {
        Task<double> DoMeasure();
    }
}

