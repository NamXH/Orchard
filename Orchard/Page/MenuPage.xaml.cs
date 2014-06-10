using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace Orchard
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            _chosenItemCmd = new Command(obj =>
            {
                var tc = (TextCell)obj;
                Debug.WriteLine("menu item tapped {0}", tc.Text);
            });

            for (var i = 0; i < 7; ++i)
            {
                var cell = new TextCell()
                {
                    Text = string.Format("Step {0}", i + 1),
                    Command = _chosenItemCmd,
                };
                cell.CommandParameter = cell;

                _calcSec.Add(cell);

            }

            _appSec.Add(new TextCell(){ Text = "About" });
            _appSec.Add(new TextCell(){ Text = "Setting" });
            _appSec.Add(new TextCell(){ Text = "Help" });
        }

        Command _chosenItemCmd;
    }
}

