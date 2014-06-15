using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Orchard
{
    public class ListingVM<T> : ViewModelBase
    {
        public ListingVM(Func<IEnumerable<T>> loadingDataFunc = null)
        {
            Models = new ObservableCollection<T>();
           

            if (loadingDataFunc != null)
            {
                var dataList = loadingDataFunc();
                foreach (var item in dataList)
                {
                    Models.Add(item);
                }
            }
            else
            {
                // Load dummy data.
                if (typeof(T) == typeof(Operator))
                {
                    var o1 = new Operator() { Name = "abc", CertificationNumber = "123", Note = "first note" };
                    var t = (T)Convert.ChangeType(o1, typeof(T));
                    // var t = (T)o1;
                    Models.Add(t);
                }
            }
        }

        public ObservableCollection<T> Models { get; private set; }
    }
}

