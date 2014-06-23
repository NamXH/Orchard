using System;
using SQLite;
using PCLStorage;

namespace Orchard
{
    public class Sprayer : NPCBase
    {
        public Sprayer Copy()
        {
            return (Sprayer)this.MemberwiseClone();
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
                return PortablePath.Combine(Helper.PictureFolderForType<Sprayer>(), string.Format("{0}.png", Id));
            }
        }

        string _model;

        public string Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        Cat _category;

        public Cat Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        string _tractor;

        public string Tractor
        {
            get { return _tractor; }
            set { SetProperty(ref _tractor, value); }
        }

        string _tractorGear;

        public string TractorGear
        {
            get { return _tractorGear; }
            set { SetProperty(ref _tractorGear, value); }
        }

        int _tractorRPM;

        public int TractorRPM
        {
            get { return _tractorRPM; }
            set { SetProperty(ref _tractorRPM, value); }
        }

        int _tankCapacity;

        public int TankCapacity
        {
            get { return _tankCapacity; }
            set { SetProperty(ref _tankCapacity, value); }
        }

        VolumeUnit _tankUnit;

        public VolumeUnit TankUnit
        {
            get { return _tankUnit; }
            set { SetProperty(ref _tankUnit, value); }
        }

        int _numOfNozzles;

        public int NumOfNozzles
        {
            get { return _numOfNozzles; }
            set { SetProperty(ref _numOfNozzles, value); }
        }

        double _refillTime;

        public double RefillTime
        {
            get { return _refillTime; }
            set { SetProperty(ref _refillTime, value); }
        }

        double _rowTurnTime;

        public double RowTurnTime
        {
            get { return _rowTurnTime; }
            set { SetProperty(ref _rowTurnTime, value); }
        }

        public enum Cat
        {
            AirblastOneSide,
            AirblastTwoSides,
            TowerTwoSide
        }

        public enum VolumeUnit
        {
            Litres,
            USGallon
        }
    }
}

