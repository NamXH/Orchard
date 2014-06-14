using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;

namespace Orchard
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage(IList<string> calcPageNames, IList<string> appPageNames)
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

            var calcTr = GetSubMenu("Calc", calcPageNames.ToArray());
            AddChangeSubmenuAction(_calc, calcTr);

            var spayerTr = GetSubMenu("Sprayer", "List of sprayers", "Add a new sprayer");
            AddChangeSubmenuAction(_sprayer, spayerTr);

            var operatorTr = GetSubMenu("Operator", "List of operators", "Add a new operator");
            AddChangeSubmenuAction(_operator, operatorTr);

            var blockTr = GetSubMenu("Orchard blocks", "List of blocks", "Add a new block");
            AddChangeSubmenuAction(_blocks, blockTr);

            var appTr = GetSubMenu("App", appPageNames.ToArray());
            AddChangeSubmenuAction(_app, appTr);

            _subMenu.Root = calcTr;
        }

        public TableRoot GetSubMenu(string sectionName, params string[] items)
        {
            var tRoot = new TableRoot();
            var tSec = new TableSection(sectionName);
            foreach (var name in items)
            {
                var cell = new TextCell()
                {
                    Text = name,
                    Command = _chosenItemCmd,
                    CommandParameter = name,
                };
                tSec.Add(cell);
            }
            tRoot.Add(tSec);

            return tRoot;
        }

        public void AddChangeSubmenuAction(View view, TableRoot subMenu)
        {
            view.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    _subMenu.Root = subMenu;
                })
            });
        }

        public event EventHandler<MenuItemChangedEventArg> MenuItemChanged;

        Command _chosenItemCmd;
    }

    public class MenuItemChangedEventArg : EventArgs
    {
        public string PageName{ get; set; }
    }
}

