using System;
using Xamarin.Forms;
using System.Diagnostics;
using PCLStorage;
using System.Collections.Generic;
using System.Linq;

namespace Orchard
{
    public class ListingPage<T> : ContentPage where T : IDataItem, new()
    {
        public ListingPage(bool inChoosingMode = false)
        {
            var vm = new ListingVM<T>();
            BindingContext = vm;

            _listView = new ListView();
            _listView.RowHeight = 50;
            _listView.ItemsSource = vm.Models;
            _listView.ItemTemplate = new DataTemplate(() =>
            {
                var ic = inChoosingMode ? new ImageCellWithCheck() : new ImageCell();
                ic.SetBinding(ImageCell.TextProperty, "Name");
                ic.SetBinding(ImageCell.ImageSourceProperty, new Binding("Image", 0, new LocalImgToImgSourceConverter()));
                return ic;
            });

            _listView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (inChoosingMode)
                {
                    var cell = ImageCellWithCheck.BindingDic[e.Item];
                    cell.Selected = !cell.Selected;
                    _listView.SelectedItem = null;

                    var handler = ItemChosen;
                    if (handler != null)
                    {
                        handler.Invoke(this, new ChosenItemEventArg<T>() { ChosenItem = (T)e.Item });
                    }
                }
                else
                {
                    NavToDetailPage((T)e.Item);
                    _listView.SelectedItem = null;
                }
            };

            Content = _listView;

            if (!inChoosingMode)
            {
                this.ToolbarItems.Add(new ToolbarItem("add", null, () =>
                {
                    NavToDetailPage(default(T));
                }));
            }
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

    public class ImageCellWithCheck : ImageCell
    {
        // HACK: get back cell object given the model/viewmodel.
        public static Dictionary<object, ImageCellWithCheck> BindingDic = new Dictionary<object, ImageCellWithCheck>();

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext != null)
            {
                BindingDic[this.BindingContext] = this;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var s1vm = App.Container.GetInstance<Step1VM>();
            if (s1vm.ChosenSprayers.Contains(BindingContext) ||
                s1vm.ChosenOrchardBlocks.Contains(BindingContext) ||
                s1vm.ChosenOperators.Contains(BindingContext))
            {
                Selected = true;
            }
        }

        public static readonly BindableProperty SelectedProperty = BindableProperty.Create<ImageCellWithCheck, bool>(curr => curr.Selected, false);

        public bool Selected
        {
            get
            {
                return (bool)base.GetValue(ImageCellWithCheck.SelectedProperty);
            }
            set
            {
                base.SetValue(ImageCellWithCheck.SelectedProperty, value);
            }
        }
    }
}

