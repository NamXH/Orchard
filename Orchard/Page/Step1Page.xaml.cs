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

//            _picker.Items.Add("a");
//            _picker.Items.Add("b");
//            _picker.Items.Add("c");
//            _picker.Items.Add("d");
//            _picker.Items.Add("e");

		}
	}
}

