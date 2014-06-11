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

        public ICommand NextPageCmd{ get; set; }

        protected override void OnAppearing()
        {
            if (_nextBtn.Command == null)
            {
                _nextBtn.Command = NextPageCmd;
                _nextBtn.CommandParameter = Title;
            }


            base.OnAppearing();
        }
    }
}

