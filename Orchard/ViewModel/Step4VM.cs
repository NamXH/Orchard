using System;
using System.Collections.ObjectModel;

namespace Orchard
{
    public class Step4VM : NPCBase
    {
        public Step4VM()
        {
        }

        ObservableCollection<Product> _products = new ObservableCollection<Product>();

        public ObservableCollection<Product> Products
        {
            get { return _products; }
        }
    }
}

