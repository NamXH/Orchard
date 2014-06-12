using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace Orchard
{
    public partial class Step1Page : ContentPage
    {
        public Step1Page()
        {
            InitializeComponent();

            BindingContext = new Step1VM();

            var tgr = new TapGestureRecognizer((view, obj) =>
            {

                _rLayout.Children.Remove(_helpSv);
                if (_showingHelp)
                {
                    // Need to hide
                    _rLayout.Children.Add(_helpSv,
                        Constraint.Constant(0),
                        Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Height - 50;
                        }),
                        Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width;
                        }),
                        Constraint.Constant(50));
                }
                else
                {
                    // Need to show
                    _rLayout.Children.Add(_helpSv,
                        Constraint.Constant(0),
                        Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Height - 200;
                        }),
                        Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width;
                        }),
                        Constraint.Constant(200));
                }
                _showingHelp = !_showingHelp;
                Debug.WriteLine("tapped");
            });



            _helpLb.GestureRecognizers.Add(tgr);


        }

        bool _showingHelp;
    }
}

