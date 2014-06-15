using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;
using System.Diagnostics;

namespace Orchard
{
    public partial class ListingPage : ContentPage
    {
        public ListingPage(ViewModelBase vm)
        {
            InitializeComponent();

            BindingContext = vm;

            _listView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                var t = e.Item.GetType();
                if (t == typeof(Operator))
                {
                    Navigation.PushAsync(new OperatorDetailPage(((Operator)e.Item)));
                }

            };

            _listView.SetBinding(ListView.ItemsSourceProperty, "Models");
        }

        protected override void OnAppearing()
        {
            //_listView.SelectedItem = -1;
        }
    }
}

