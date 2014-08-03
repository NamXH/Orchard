using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{	
	public partial class Step2Page : ContentPage
	{	
		public Step2Page ()
		{
			InitializeComponent ();

            foreach (var str in GrowthStageToPickerIdxCov.Names)
            {
                _growthStage.Items.Add(str);
            }

            BindingContext = App.Container.GetInstance<Step2VM>();

            ViewUtils.SetupStepView(_rLayout, _helpSv, _questionContainer, VM.Common.QuestionTapped);
		}

        Step2VM VM
        {
            get
            {
                return (Step2VM)BindingContext;
            }
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
	}
}

