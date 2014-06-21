using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.IO;
using PCLStorage;

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
                }
                Navigation.PopAsync();
            }));
        }

        Operator _currItem;
        string _opFolder = "operators";

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
                var checkFolder = await FileSystem.Current.LocalStorage.CheckExistsAsync(_opFolder);
                if (checkFolder == ExistenceCheckResult.NotFound)
                {
                    await FileSystem.Current.LocalStorage.CreateFolderAsync(_opFolder, CreationCollisionOption.FailIfExists);
                }
                if (_currItem.Id == 0)
                {
                    // TODO: get next id from db and assign it here.

                }
                var relativeFilename = PortablePath.Combine(_opFolder, string.Format("{0}.png", _currItem.Id));
                var localFile = await FileSystem.Current.LocalStorage.CreateFileAsync(
                                    relativeFilename,
                                    CreationCollisionOption.ReplaceExisting);
                using (var lfStream = await localFile.OpenAsync(FileAccess.ReadAndWrite))
                {
                    await photoStream.CopyToAsync(lfStream);
                }
                _currItem.Image = relativeFilename;
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

        public void IssueNeedRefreshData()
        {
            var handler = NeedRefreshData;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

