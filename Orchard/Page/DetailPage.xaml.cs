using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;

namespace Orchard
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(IDataItem currItem, Type currType)
        {
            InitializeComponent();

            if (currType == typeof(Operator))
            {
                BindingContext = new DetailVM<Operator>(x => x.Copy(), (x, y) =>
                {
                    x.Name = y.Name;
                    x.CertificationNumber = y.CertificationNumber;
                    x.Note = y.Note;
                })
                {
                    CurrItem = (Operator)currItem
                };
            }
            else if (currType == typeof(Sprayer))
            {
                BindingContext = new DetailVM<Sprayer>(x => x.Copy(), (x, y) =>
                {
                    x.Name = y.Name;
//                    x.CertificationNumber = y.CertificationNumber;
//                    x.Note = y.Note;
                })
                {
                    CurrItem = (Sprayer)currItem
                };
            }
            else if (currType == typeof(OrchardBlock))
            {
                BindingContext = new DetailVM<OrchardBlock>(x => x.Copy(), (x, y) =>
                {
                    x.Name = y.Name;
//                    x.CertificationNumber = y.CertificationNumber;
//                    x.Note = y.Note;
                })
                {
                    CurrItem = (OrchardBlock)currItem
                };
            }


            ToolbarItems.Add(new ToolbarItem("Done", null, () =>
            {
                VM.DoneCmd.Execute(null);
                if (!VM.IsEditing)
                {
                    IssueNeedRefreshData();
                }
                Navigation.PopAsync();
            }));
        }

        dynamic VM
        {
            get { return (dynamic)BindingContext; }
        }

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

            var t = (Task)VM.ChangeImg(photoStream);
            await t;
            var li = (NPCBase)VM.LocalItem;
            li.RaisePropertyChanged("Image");
            // HACK: binding is not working, set manually.
            ((ImageButton)sender).Image = null;
            ((ImageButton)sender).Image = VM.LocalItem.Image;
        }

        public async void DelClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(null, "Cancel", "Delete?");
            if (action == "Delete?")
            {
                VM.DelCmd.Execute(null);
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
    }
}

