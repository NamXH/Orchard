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

            var tgr = new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    if (_showingHelp)
                    {
                        // Need to hide
                        var newBound = new Rectangle(0, _rLayout.Height - 50, _rLayout.Width, 50);
                        _helpSv.LayoutTo(newBound, 250, Easing.CubicInOut);
                    }
                    else
                    {
                        // Need to show
                        var newBound = new Rectangle(0, _rLayout.Height - 200, _rLayout.Width, 200);
                        _helpSv.LayoutTo(newBound, 250, Easing.CubicInOut);
                    }
                    _showingHelp = !_showingHelp;
                })
            };


            _helpSv.GestureRecognizers.Add(tgr);
        }

        bool _showingHelp;
    }
}

