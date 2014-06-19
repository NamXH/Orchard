using System;
using Xamarin.Forms;

namespace Orchard
{
    public class ListingPage<T> : ContentPage where T : new()
    {
        public ListingPage()
        {
            var vm = new ListingVM<T>();
            BindingContext = vm;

            var listView = new ListView();
            listView.ItemsSource = vm.Models;
            listView.ItemTemplate = new DataTemplate(() =>
            {
                var ic = new ImageCell();
                ic.SetBinding(ImageCell.TextProperty, "Name");

                return ic;
            });

            listView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                var t = e.Item.GetType();
                if (t == typeof(Operator))
                {
                    Navigation.PushAsync(new OperatorDetailPage(((Operator)e.Item)));
                }

            };

            Content = listView;
        }

        ListingVM<T> ViewModel
        {
            get
            {
                return (ListingVM<T>)BindingContext;
            }
        }
    }
}

