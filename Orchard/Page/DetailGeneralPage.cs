using System;
using Xamarin.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Orchard
{
    public class DetailGeneralPage<T> : ContentPage where T : IDataItem, new()
    {
        public DetailGeneralPage(T currItem)
        {
            var dgv = new DetailGeneralView();
            dgv.SetupHandlers(ImageClicked, DelClicked);
            Content = dgv;
            _sl = dgv.MainStackLayout;


            var currType = typeof(T);

            if (currType == typeof(Operator))
            {
                BindingContext = new DetailVM<Operator>(x => x.Copy(), (x, y) =>
                {
                    x.Name = y.Name;
                    x.CertificationNumber = y.CertificationNumber;
                    x.Note = y.Note;
                })
                {
                    CurrItem = (Operator)((object)currItem)
                };
            }
            else if (currType == typeof(Sprayer))
            {
                BindingContext = new DetailVM<Sprayer>(x => x.Copy(), (x, y) =>
                {
                    x.Name = y.Name;
                    x.Model = y.Model;
                    x.Category = y.Category;
                    x.Tractor = y.Tractor;
                    x.TractorGear = y.TractorGear;
                    x.TractorRPM = y.TractorRPM;
                    x.TankCapacity = y.TankCapacity;
                    x.TankCapacityUnit = y.TankCapacityUnit;
                    x.NumOfNozzles = y.NumOfNozzles;
                    x.RefillTime = y.RefillTime;
                    x.RowTurnTime = y.RowTurnTime;
                })
                {
                    CurrItem = (Sprayer)((object)currItem)
                };
            }
            else if (currType == typeof(OrchardBlock))
            {
                BindingContext = new DetailVM<OrchardBlock>(x => x.Copy(), (x, y) =>
                {
                    x.Name = y.Name;
                    x.VarietiesPlanted = y.VarietiesPlanted;
                    x.RootStock = y.RootStock;
                    x.YearPlanted = y.YearPlanted;
                    x.BlockSize = y.BlockSize;
                    x.BlockSizeUnit = y.BlockSizeUnit;
                    x.AvgTreeHeight = y.AvgTreeHeight;
                    x.AvgTreeHeightUnit = y.AvgTreeHeightUnit;
                    x.AvgCanopyWidth = y.AvgCanopyWidth;
                    x.AvgCanopyWidthUnit = y.AvgCanopyWidthUnit;
                    x.AvgRowLength = y.AvgRowLength;
                    x.AvgRowLengthUnit = y.AvgRowLengthUnit;
                    x.AvgTreeShape = y.AvgTreeShape;
                })
                {
                    CurrItem = (OrchardBlock)((object)currItem)
                };
            }
            SetupUIForType(currType);


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

        DetailVM<T> VM
        {
            get { return (DetailVM<T>)BindingContext; }
        }

        StackLayout _sl;

        void SetupUIForType(Type currType)
        {
            View subView = null;
            if (currType == typeof(Operator))
            {
                subView = new DetailOperatorView();

            }
            else if (currType == typeof(Sprayer))
            {
                subView = new DetailSprayerView();
            }
            else if (currType == typeof(OrchardBlock))
            {
                subView = new DetailOrchardBlockView();
            }
            _sl.Children.Insert(1, subView);
        }

        public async void ImageClicked(object sender, EventArgs e)
        {
            var actionList = new string[] { "Take Photo", "Choose Photo" };
            var action = await DisplayActionSheet(null, "Cancel", "Remove", actionList);
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
            else if (action == "Remove")
            {
                // Pass null to ChangeImg to delete.
                await (Task)VM.ChangeImg(null);
                ((NPCBase)((object)VM.LocalItem)).RaisePropertyChanged("Image");
                // HACK: binding is not working, set manually.
                ((ImageButton)sender).Image = null;
                ((ImageButton)sender).Image = VM.LocalItem.Image;
                return;
            }

            if (photoStream == null)
            {
                return;
            }

            await VM.ChangeImg(photoStream);
            ((NPCBase)((object)VM.LocalItem)).RaisePropertyChanged("Image");
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

