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

                var newOp = new Operator();
                BindingContext = newOp;
            }
            else
            {
                // Editing mode.
                BindingContext = current;
            }

            _addBtn.Clicked += (object sender, EventArgs e) =>
            {
                var op = (Operator)BindingContext;
                DbManager.AddItem(op);
            };

            _cancelBtn.Clicked += (object sender, EventArgs e) =>
            {
                Debug.WriteLine("Cancel clicked");


            };
        }

        public async void ImageClicked(object sender, EventArgs e)
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
}

