using System;
using Xamarin.Forms;
using System.Diagnostics;
using PCLStorage;

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
                ic.SetBinding(ImageCell.ImageSourceProperty, new Binding("Image", 0, new LocalImgToImgSourceConverter()));
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

    public class LocalImgToImgSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var localImgSource = (string)value;
            if (localImgSource == null)
            {
                return null;
            }
            var imgSource = PortablePath.Combine(FileSystem.Current.LocalStorage.Path, localImgSource);
            return imgSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

