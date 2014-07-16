using System;

namespace Orchard
{
    public class Product : NPCBase
    {
        string _name;

        public string Name
        { 
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        string _labelledRate;

        public string LablledRate
        { 
            get { return _labelledRate; }
            set { SetProperty(ref _labelledRate, value); }
        }

        string _cost;

        public string Cost
        { 
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }
    }
}

