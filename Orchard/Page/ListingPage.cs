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
            _listView.RowHeight = 50;
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
                NavToDetailPage((T)e.Item);
                _listView.SelectedItem = null;
            };

            Content = _listView;

            this.ToolbarItems.Add(new ToolbarItem("add", null, () =>
            {
                NavToDetailPage(default(T));
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

        void NavToDetailPage(T currItem)
        {
            var detailPage = new DetailPage((IDataItem)currItem, typeof(T));
            detailPage.NeedRefreshData += ViewModel.OnNeedRefreshData;
            Navigation.PushAsync(detailPage);
        }
    }

    public class LocalImgToImgSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var localImgSource = (string)value;
            var imgSource = localImgSource == null ? null : PortablePath.Combine(FileSystem.Current.LocalStorage.Path, localImgSource);
            return imgSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

