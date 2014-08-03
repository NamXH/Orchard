using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace Orchard
{
    public partial class Step4Page : ContentPage
    {
        public Step4Page()
        {
            InitializeComponent();

            BindingContext = App.Container.GetInstance<Step4VM>();

            this.ToolbarItems.Add(new ToolbarItem("add", null, () =>
            {
                VM.Products.Add(new Product(){ Name = "abc" });
            }));


        }

        Step4VM VM
        {
            get
            {
                return (Step4VM)BindingContext;
            }
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
    }

    public class NonSelectableListView : ListView
    {

    }
}

