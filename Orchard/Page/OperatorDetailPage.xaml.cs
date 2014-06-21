using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace Orchard
{
    public partial class OperatorDetailPage : ContentPage
    {
        public OperatorDetailPage(Operator current)
        {
            InitializeComponent();

            if (current == null)
            {
                // Adding a new one. TODO
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
                BindingContext = current;
            }

            _addBtn.Clicked += (object sender, EventArgs e) =>
            {
                var op = (Operator)BindingContext;
                DbManager.AddItem(op);
            };
        }

        public async void ImageClicked(object sender, EventArgs e)
        {
            var actionList = new string[] { "Take Photo", "Choose Photo" };
            var action = await DisplayActionSheet(null, "Cancel", null, actionList);

            if (action == actionList[0])
            {
                // TODO.
            }
            else if (action == actionList[1])
            {
                var picker = DependencyService.Get<IPhotoPicker>();
                var res = await picker.Show();
                        
                var f = await PCLStorage.FileSystem.Current.GetFileFromPathAsync(res);
                using (var fs = await f.OpenAsync(PCLStorage.FileAccess.Read))
                {
                    Debug.WriteLine("File length: {0}", fs.Length);
                }
            }
        }

        public void CancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}

