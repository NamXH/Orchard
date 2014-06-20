using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace Orchard
{
    public class ListingPage<T> : ContentPage where T : new()
    {
        public ListingPage()
        {
            var vm = new ListingVM<T>();
            BindingContext = vm;

            _listView = new ListView();
            _listView.ItemsSource = vm.Models;
            _listView.ItemTemplate = new DataTemplate(() =>
            {
                var ic = new ImageCell();
                ic.SetBinding(ImageCell.TextProperty, "Name");

                return ic;
            });

            _listView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                var t = e.Item.GetType();
                if (t == typeof(Operator))
                {
                    Navigation.PushAsync(new OperatorDetailPage(((Operator)e.Item)));
                }

            };

            Content = _listView;

            this.ToolbarItems.Add(new ToolbarItem("add", null, () =>
            {
                Debug.WriteLine("add");
            }));
        }

        ListView _listView;

        ListingVM<T> ViewModel
        {
            get
            {
                return (ListingVM<T>)BindingContext;
            }
        }
    }
}

