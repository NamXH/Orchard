using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{	
	public partial class Step3Page : ContentPage
	{	
		public Step3Page ()
		{
			InitializeComponent ();

            var vm = new Step3VM();

            BindingContext = vm;

            ViewUtils.SetupStepView(_rLayout, _helpSv, _questionContainer, vm.Common.QuestionTapped);
		}

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
	}
}

