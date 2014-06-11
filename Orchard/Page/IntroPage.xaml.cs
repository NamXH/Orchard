using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{	
    public partial class IntroPage : ContentPage
	{	
		public IntroPage ()
		{
			InitializeComponent ();

            var str = Helper.ReadTextData("Intro.txt");

            _lb1.Text = str;
		}
	}
}

