using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;

namespace Orchard
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            _chosenItemCmd = new Command(obj =>
            {
                var mic = MenuItemChanged;
                if (mic != null)
                {
                    mic.Invoke(this, new MenuItemChangedEventArg()
                    {
                        SelectedMenuItem = obj as MenuItem,
                    });
                }
            });

            _calcMenuItems = new List<MenuItem>()
            {
                new MenuItem("Introduction", () => new IntroPage()),
                new MenuItem("Step 1", () => new Step1Page()),
                new MenuItem("Step 2", () => new Step2Page()),
                new MenuItem("Step 3", () => new Step3Page()),
                new MenuItem("Step 4", () => new ContentPage()),
                new MenuItem("Step 5", () => new ContentPage()),
            };

            var calcTr = GetSubMenu("Calc", _calcMenuItems.ToArray());
//            TwitterButton.Clicked += (object sender, EventArgs e) =>
//            {
//                Debug.WriteLine("clicked");
//            };
            //AddChangeSubmenuAction(_calc, calcTr);

            var sLVM = new ListingVM<Sprayer>();
            var sprayerMenuItems = new List<MenuItem>()
            {
                new MenuItem("List of sprayers", () => new ListingPage<Sprayer>(sLVM)),
                new MenuItem("Add a new sprayer", () => new ContentPage()),
            };
            var spayerTr = GetSubMenu("Sprayer", sprayerMenuItems.ToArray());
            AddChangeSubmenuAction(_sprayer, spayerTr);

            var operatorLVM = new ListingVM<Operator>();
            var operatorMenuItems = new List<MenuItem>()
            {
                new MenuItem("List of operator", () => new ListingPage<Operator>(operatorLVM)),
                new MenuItem("Add a new operator", () => new OperatorDetailPage(null)),
            };
            var operatorTr = GetSubMenu("Operator", operatorMenuItems.ToArray());
            AddChangeSubmenuAction(_operator, operatorTr);

            var orchardBlockMenuItems = new List<MenuItem>()
            {
                new MenuItem("List of orchard blocks", () => new ListingPage<OrchardBlock>(new ListingVM<OrchardBlock>())),
                new MenuItem("Add a new orchard block", () => new ContentPage()),
            };
            var blockTr = GetSubMenu("Orchard blocks", orchardBlockMenuItems.ToArray());
            AddChangeSubmenuAction(_blocks, blockTr);

            var appMenuItems = new List<MenuItem>()
            {
                new MenuItem("About", () => new ContentPage()),
                new MenuItem("Settings", () => new ContentPage()),
                new MenuItem("Help", () => new ContentPage()),
            };
            var appTr = GetSubMenu("App", appMenuItems.ToArray());
            AddChangeSubmenuAction(_app, appTr);

            _subMenu.Root = calcTr;
        }

        List<MenuItem> _calcMenuItems;

        public Page DefaultPage
        {
            get
            {
                return _calcMenuItems[0].NaviPage;
            }
        }

        public Page GetNextCalcPage(string currPageTitle)
        {
            var nextMenuItem = _calcMenuItems.SkipWhile(x => x.MenuTitle != currPageTitle).Skip(1).First();
            return nextMenuItem.NaviPage;
        }

        public TableRoot GetSubMenu(string sectionName, params MenuItem[] items)
        {
            var tRoot = new TableRoot();
            var tSec = new TableSection(sectionName);

            foreach (var item in items)
            {
                var cell = new TextCell()
                {
                    Text = item.MenuTitle,
                    Command = _chosenItemCmd,
                    CommandParameter = item,
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
        public MenuItem SelectedMenuItem { get; set; }
    }
}

