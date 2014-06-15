using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{
    public partial class OperatorDetailPage : ContentPage
    {
        public OperatorDetailPage(Operator current)
        {
            InitializeComponent();

            if (current == null)
            {
                // Adding a new one. TODO
            }
            else
            {
                // Editing mode.
                BindingContext = current;
            }
        }
    }
}

