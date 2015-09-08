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
                new MenuItem("Step 4", () => new Step4Page()),
                new MenuItem("Step 5", () => new ContentPage()),
            };

            _appMenuItems = new List<MenuItem>()
            {
                new MenuItem("About", () => new ContentPage()),
                new MenuItem("Settings", () => new ContentPage()),
                new MenuItem("Help", () => new ContentPage()),
            };

            _subMenu.ItemsSource = _calcMenuItems;

            _subMenu.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                if (e.SelectedItem != null)
                {
                    var mi = (MenuItem)e.SelectedItem;
                    _subMenu.SelectedItem = null;
                    _chosenItemCmd.Execute(mi);
                }
            };

            _subMenu.ItemTemplate = new DataTemplate(() =>
            {
                var tc = new TextCell();
                tc.SetBinding(TextCell.TextProperty, "MenuTitle");

                return tc;
            });
        }

        List<MenuItem> _calcMenuItems;
        List<MenuItem> _appMenuItems;

//        MenuItem _sprayerList = new MenuItem("List of sprayers", () => new ListingPage<Sprayer>());
//        MenuItem _operatorList = new MenuItem("List of operator", () => new ListingPage<Operator>());
//        MenuItem _blockList = new MenuItem("List of orchard blocks", () => new ListingPage<OrchardBlock>());

        MenuItem _sprayerList = new MenuItem("List of sprayers", () => new Page());
        MenuItem _operatorList = new MenuItem("List of operator", () => new Page());
        MenuItem _blockList = new MenuItem("List of orchard blocks", () => new Page());

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

        public void ChangeSubMenuClicked(object sender, EventArgs e)
        {
            var ib = (ImageButton)sender;
            switch (ib.Text)
            {
                case "Calculate":
                    _subMenu.ItemsSource = _calcMenuItems;
                    break;
                case "App":
                    _subMenu.ItemsSource = _appMenuItems;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void ListingClicked(object sender, EventArgs e)
        {
            var ib = (ImageButton)sender;
            switch (ib.Text)
            {
                case "Sprayer":
                    _chosenItemCmd.Execute(_sprayerList);
                    break;
                case "Operator":
                    _chosenItemCmd.Execute(_operatorList);
                    break;
                case "Orchard Blocks":
                    _chosenItemCmd.Execute(_blockList);
                    break;
                default:
                    throw new InvalidOperationException();
            }
            _subMenu.ItemsSource = null;
        }

        public event EventHandler<MenuItemChangedEventArg> MenuItemChanged;

        Command _chosenItemCmd;
    }

    public class MenuItemChangedEventArg : EventArgs
    {
        public MenuItem SelectedMenuItem { get; set; }
    }
}

