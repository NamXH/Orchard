using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;

namespace Orchard
{
    public partial class Step1Page : ContentPage
    {
        public Step1Page()
        {
            InitializeComponent();

            var vm = new Step1VM();

            BindingContext = vm;

            ViewUtils.SetupStepView(_rLayout, _helpSv, _questionContainer, vm.Common.QuestionTapped);
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
    }
}

