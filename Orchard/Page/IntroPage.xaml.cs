using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using System.Diagnostics;

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

        public async void MeasureClicked(object sender, EventArgs e)
        {
            var mt = DependencyService.Get<IMeasureTool>();

            var x = await mt.DoMeasure();

            Debug.WriteLine("x: {0}", x);
        }
    }
}

