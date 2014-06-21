using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

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
                _addBtn.IsVisible = true;
                _delBtn.IsVisible = false;
                var newOp = new Operator();
                BindingContext = newOp;
            }
            else
            {
                // Editing mode.
                _addBtn.IsVisible = false;
                _delBtn.IsVisible = true;

                var localCurrItem = currItem.Copy();
                BindingContext = localCurrItem;
            }
        }

        Operator _currItem;
        bool _actionKeyPressed;

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (_currItem != null && !_actionKeyPressed)
            {
                // Not a result of pressing add/del/cancel button.
                // Editing mode, need to save.
                var op = (Operator)BindingContext;
                DbManager.Update(op);
                // Update all needed properties.
                _currItem.Name = op.Name;
            }
        }

        public async void ImageClicked(object sender, EventArgs e)
        {
            var actionList = new string[] { "Take Photo", "Choose Photo" };
            var action = await DisplayActionSheet(null, "Cancel", null, actionList);

            if (action == actionList[0])
            {
                var picker = DependencyService.Get<IMediaPicker>();
                using (var res = await picker.TakePhoto())
                {
                    if (res != null)
                    {
                        Debug.WriteLine("Length: {0}", res.Length);
                    }
                }
            }
            else if (action == actionList[1])
            {
                var picker = DependencyService.Get<IMediaPicker>();
                        
                using (var res = await picker.PickPhoto())
                {
                    if (res != null)
                    {
                        Debug.WriteLine("Length: {0}", res.Length);
                    }
                }
            }
        }

        public void AddClicked(object sender, EventArgs e)
        {
            var op = (Operator)BindingContext;
            DbManager.AddItem(op);
            IssueNeedRefreshData();
            _actionKeyPressed = true;
            Navigation.PopAsync();
        }

        public async void DelClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(null, "Cancel", "Delete?");
            if (action == "Delete?")
            {
                var op = (Operator)BindingContext;
                DbManager.DeleteItem(op);
                IssueNeedRefreshData();
                _actionKeyPressed = true;
                Navigation.PopAsync();
            }
        }

        public void CancelClicked(object sender, EventArgs e)
        {
            _actionKeyPressed = true;
            Navigation.PopAsync();
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

