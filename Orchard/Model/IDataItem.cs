using System;

namespace Orchard
{
    public interface IDataItem
    {
        int Id { get; }

        string Name{ get; }

        string Image { get; }
    }
}

