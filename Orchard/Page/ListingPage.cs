using System;
using Xamarin.Forms;
using System.Diagnostics;
using PCLStorage;

namespace Orchard
{
    public class ListingPage<T> : ContentPage where T : IDataItem, new()
    {
        public ListingPage(bool choosingMode = false)
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
                if (choosingMode)
                {
                    var handler = ItemChosen;
                    if (handler != null)
                    {
                        handler.Invoke(this, new ChosenItemEventArg<T>() { ChosenItem = (T)e.Item });
                    }
                    Navigation.PopAsync();
                }
                else
                {
                    NavToDetailPage((T)e.Item);
                    _listView.SelectedItem = null;
                }
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
            var detailPage = new DetailGeneralPage<T>(currItem);
            detailPage.NeedRefreshData += ViewModel.OnNeedRefreshData;
            Navigation.PushAsync(detailPage);
        }

        public event EventHandler<ChosenItemEventArg<T>> ItemChosen;
    }

    public class ChosenItemEventArg<T> : EventArgs
    {
        public T ChosenItem { get; set; }
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

