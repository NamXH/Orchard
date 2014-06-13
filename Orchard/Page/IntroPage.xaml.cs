using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace Orchard
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();

            var str = Helper.ReadTextData("Intro.txt");

            _lb1.Text = str;
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
    }
}

