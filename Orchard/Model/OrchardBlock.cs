using System;
using SQLite;
using PCLStorage;

namespace Orchard
{
    public class OrchardBlock : NPCBase
    {
        public OrchardBlock Copy()
        {
            return (OrchardBlock)this.MemberwiseClone();
        }

        int _id;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        string _name;

        public string Name
        { 
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Image
        { 
            get
            {
                return PortablePath.Combine(Helper.PictureFolderForType<OrchardBlock>(), string.Format("{0}.png", Id));
            }
        }

        string _varietiesPlanted;

        public string VarietiesPlanted
        { 
            get { return _varietiesPlanted; }
            set { SetProperty(ref _varietiesPlanted, value); }
        }

        string _rootStock;

        public string RootStock
        { 
            get { return _rootStock; }
            set { SetProperty(ref _rootStock, value); }
        }

        DateTime _yearPlanted;

        public DateTime YearPlanted
        {
            get { return _yearPlanted; }
            set { SetProperty(ref _yearPlanted, value); }
        }

        AreaUnit _blockSize;

        public AreaUnit BlockSize
        {
            get { return _blockSize; }
            set { SetProperty(ref _blockSize, value); }
        }

        LengthUnit _avgTreeHeight;

        public LengthUnit AvgTreeHeight
        {
            get { return _avgTreeHeight; }
            set { SetProperty(ref _avgTreeHeight, value); }
        }

        LengthUnit _avgCanopyWidth;

        public LengthUnit AvgCanopyWidth
        {
            get { return _avgCanopyWidth; }
            set { SetProperty(ref _avgCanopyWidth, value); }
        }

        LengthUnit _avgRowLength;

        public LengthUnit AvgRowLength
        {
            get { return _avgRowLength; }
            set { SetProperty(ref _avgRowLength, value); }
        }

        TreeShape _avgTreeShape;

        public TreeShape AvgTreeShape
        {
            get { return _avgTreeShape; }
            set { SetProperty(ref _avgTreeShape, value); }
        }

        public enum AreaUnit
        {
            Hectares,
            Acres
        }

        public enum LengthUnit
        {
            Metres,
            Feet
        }

        public enum TreeShape
        {
            Cube,
            blah
        }
    }
}

