﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{	
	public partial class Step2Page : ContentPage
	{	
		public Step2Page ()
		{
			InitializeComponent ();

            BindingContext = new Step2VM();
		}
	}
}
