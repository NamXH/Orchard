﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Orchard
{
    public class ListingVM<T>  : NPCBase where T : new()
    {
        public ListingVM()
        {
            Models = new ObservableCollection<T>();
           
            RefreshData();
        }

        public void RefreshData()
        {
            Models.Clear();

            var itemList = DbManager.GetTable<T>();
            foreach (var item in itemList)
            {
                Models.Add(item);
            }
        }

        public void OnNeedRefreshData(object sender, EventArgs e)
        {
            RefreshData();
        }

        public ObservableCollection<T> Models { get; private set; }
    }
}

