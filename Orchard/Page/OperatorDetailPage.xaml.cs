using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.IO;
using PCLStorage;
using System.Linq.Expressions;
using System.Reflection;

namespace Orchard
{
    public partial class OperatorDetailPage : ContentPage
    {
        public OperatorDetailPage(Operator currItem)
        {
            InitializeComponent();
            _currItem = currItem;
            if (currItem == null)
            {
                // Adding a new one.
                _delBtn.IsVisible = false;
                var newOp = new Operator();
                BindingContext = newOp;
            }
            else
            {
                // Editing mode.
                var localCurrItem = currItem.Copy();
                BindingContext = localCurrItem;
            }

            ToolbarItems.Add(new ToolbarItem("Done", null, () =>
            {
                var op = (Operator)BindingContext;
                if (_currItem == null)
                {
                    // Adding a new item.
                    DbManager.AddItem(op);
                    IssueNeedRefreshData();
                }
                else
                {
                    // Editing an existing item.
                    DbManager.Update(op);
                    // Update all needed properties.
                    _currItem.Name = op.Name;
                    _currItem.CertificationNumber = op.CertificationNumber;
                }
                Navigation.PopAsync();
            }));

            var vList = BuildViews<Operator, string>(curr => curr.CertificationNumber);
            foreach (var view in vList)
            {
                _sl.Children.Add(view);
            }

        }

        Operator _currItem;

        public async void ImageClicked(object sender, EventArgs e)
        {
            var actionList = new string[] { "Take Photo", "Choose Photo" };
            var action = await DisplayActionSheet(null, "Cancel", null, actionList);
            var picker = DependencyService.Get<IMediaPicker>();
            Stream photoStream = null;

            if (action == actionList[0])
            {
                photoStream = await picker.TakePhoto();
            }
            else if (action == actionList[1])
            {
                photoStream = await picker.PickPhoto();
            }
            if (photoStream == null)
            {
                return;
            }
            using (photoStream)
            {
                Debug.WriteLine("Length: {0}", photoStream.Length);
                var checkFolder = await FileSystem.Current.LocalStorage.CheckExistsAsync(Helper.PictureFolderForType<Operator>());
                if (checkFolder == ExistenceCheckResult.NotFound)
                {
                    await FileSystem.Current.LocalStorage.CreateFolderAsync(Helper.PictureFolderForType<Operator>(), CreationCollisionOption.FailIfExists);
                }
                if (_currItem.Id == 0)
                {
                    // TODO: get next id from db and assign it here.

                }
                var relativeFilename = _currItem.Image;
                var localFile = await FileSystem.Current.LocalStorage.CreateFileAsync(
                                    relativeFilename,
                                    CreationCollisionOption.ReplaceExisting);
                using (var lfStream = await localFile.OpenAsync(FileAccess.ReadAndWrite))
                {
                    await photoStream.CopyToAsync(lfStream);
                }

                _currItem.RaisePropertyChanged("Image");
                // HACK: binding is not working, set manually.
                ((ImageButton)sender).Image = null;
                ((ImageButton)sender).Image = _currItem.Image;
            }
        }

        public async void DelClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(null, "Cancel", "Delete?");
            if (action == "Delete?")
            {
                var op = (Operator)BindingContext;
                DbManager.DeleteItem(op);
                IssueNeedRefreshData();
                Navigation.PopAsync();
            }
        }

        public event EventHandler<EventArgs> NeedRefreshData;

        private void IssueNeedRefreshData()
        {
            var handler = NeedRefreshData;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        private IList<View> BuildViews<ItemType, PropertyType>(Expression<Func<ItemType, PropertyType>> getter)
        {
            if (getter == null)
            {
                throw new ArgumentNullException("getter");
            }
            var expression = getter.Body;
            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                expression = unaryExpression.Operand;
            }
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("getter must be a MemberExpression", "getter");
            }
            var propertyInfo = (PropertyInfo)memberExpression.Member;

            if (propertyInfo.PropertyType == typeof(string))
            {
                var ret = new List<View>();
                ret.Add(new Label() { Text = propertyInfo.Name });
                var entry = new Entry();
                entry.SetBinding(Entry.TextProperty, propertyInfo.Name);
                ret.Add(entry);
                return ret;
            }
            return null;
        }
    }
}

