using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace Orchard
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage(IList<string> caclPageNames, IList<string> appPageNames)
        {
            InitializeComponent();

            _chosenItemCmd = new Command(obj =>
            {
                var mic = MenuItemChanged;
                if (mic != null)
                {
                    mic.Invoke(this, new MenuItemChangedEventArg()
                    {
                        PageName = obj as string,
                    });
                }
            });

            foreach (var name in caclPageNames)
            {
                var cell = new TextCell()
                {
                    Text = name,
                    Command = _chosenItemCmd,
                    CommandParameter = name,
                };
                _calcSec.Add(cell);
            }

            foreach (var name in appPageNames)
            {
                var cell = new TextCell()
                {
                    Text = name,
                    Command = _chosenItemCmd,
                    CommandParameter = name,
                };
                _appSec.Add(cell);
            }
        }

        public event EventHandler<MenuItemChangedEventArg> MenuItemChanged;

        Command _chosenItemCmd;
    }

    public class MenuItemChangedEventArg : EventArgs
    {
        public string PageName{ get; set; }
    }
}

