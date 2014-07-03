using System;
using SQLite;
using PCLStorage;

namespace Orchard
{
    public class OrchardBlock : NPCBase, IDataItem
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

        double _blockSize;

        public double BlockSize
        {
            get { return _blockSize; }
            set { SetProperty(ref _blockSize, value); }
        }

        AreaUnit _blockSizeUnit;

        public AreaUnit BlockSizeUnit
        {
            get { return _blockSizeUnit; }
            set { SetProperty(ref _blockSizeUnit, value); }
        }

        double _avgTreeHeight;

        public double AvgTreeHeight
        {
            get { return _avgTreeHeight; }
            set { SetProperty(ref _avgTreeHeight, value); }
        }

        LengthUnit _avgTreeHeightUnit;

        public LengthUnit AvgTreeHeightUnit
        {
            get { return _avgTreeHeightUnit; }
            set { SetProperty(ref _avgTreeHeightUnit, value); }
        }

        double _avgCanopyWidth;

        public double AvgCanopyWidth
        {
            get { return _avgCanopyWidth; }
            set { SetProperty(ref _avgCanopyWidth, value); }
        }

        LengthUnit _avgCanopyWidthUnit;

        public LengthUnit AvgCanopyWidthUnit
        {
            get { return _avgCanopyWidthUnit; }
            set { SetProperty(ref _avgCanopyWidthUnit, value); }
        }

        double _avgRowLength;

        public double AvgRowLength
        {
            get { return _avgRowLength; }
            set { SetProperty(ref _avgRowLength, value); }
        }

        LengthUnit _avgRowLengthUnit;

        public LengthUnit AvgRowLengthUnit
        {
            get { return _avgRowLengthUnit; }
            set { SetProperty(ref _avgRowLengthUnit, value); }
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

