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
                    var odPage = new OperatorDetailPage((Operator)e.Item);
                    odPage.NeedRefreshData += ViewModel.OnNeedRefreshData;
                    Navigation.PushAsync(odPage);
                }
                _listView.SelectedItem = null;
            };

            Content = _listView;

            this.ToolbarItems.Add(new ToolbarItem("add", null, () =>
            {
                var t = typeof(T);
                if (t == typeof(Operator))
                {
                    var odPage = new OperatorDetailPage(null);
                    odPage.NeedRefreshData += ViewModel.OnNeedRefreshData;
                    Navigation.PushAsync(odPage);
                }
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

