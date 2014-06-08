using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{	
	public partial class Step1Page : ContentPage
	{	
		public Step1Page ()
		{
			InitializeComponent ();

            BindingContext = new Step1VM();
		}
	}
}

