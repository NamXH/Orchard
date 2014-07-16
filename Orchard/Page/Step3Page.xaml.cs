using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{
    public partial class Step3Page : ContentPage
    {
        public Step3Page()
        {
            InitializeComponent();

            var vm = new Step3VM();

            BindingContext = vm;

            //ViewUtils.SetupStepView(_rLayout, _helpSv, _questionContainer, vm.Common.QuestionTapped);

            foreach (var str in PressureUnitToPickerIdxCov.Names)
            {
                _pressureUnit.Items.Add(str);
            }

            foreach (var str in SpeedUnitToPickerIdxCov.Names)
            {
                _speedUnit.Items.Add(str);
            }
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
    }
}

