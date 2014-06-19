using System;

namespace Orchard
{
    public class Sprayer : NPCBase
    {
        string _name;

        public string Name
        { 
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}

