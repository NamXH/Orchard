using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;
using System.Diagnostics;

namespace Orchard
{
    public partial class ListingPage : ContentPage
    {
        public ListingPage()
        {
            InitializeComponent();

            _listView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                var t = e.Item.GetType();
                Debug.WriteLine(t.Name);
            };
        }

        public IEnumerable ItemSource
        {
            set
            {
                _listView.ItemsSource = value;
            }
        }
    }
}

