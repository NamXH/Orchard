using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;

namespace Orchard
{
    public partial class Step1Page : ContentPage
    {
        public Step1Page()
        {
            InitializeComponent();

            foreach (var str in OptimizeModeToPickerIdxCov.Names)
            {
                _opMode.Items.Add(str);
            }

            foreach (var str in RowSprayingModeToPickerIdxCov.Names)
            {
                _rowSprayingMode.Items.Add(str);
            }

            BindingContext = App.Container.GetInstance<Step1VM>();

            ViewUtils.SetupStepView(_rLayout, _helpSv, _questionContainer, VM.Common.QuestionTapped);

            var sprayerLP = new ListingPage<Sprayer>(true);
            sprayerLP.ItemChosen += (sender, arg) =>
            {
                VM.CurrSprayer = arg.ChosenItem;
            };
            var sprayerAIO = new AddItemOption()
            { 
                CurrItemPropertyName = "CurrSprayer",
                MenuItem = new MenuItem(L10n.Localize("Sprayers", null), () => sprayerLP)
            };

            var orchardBlockLP = new ListingPage<OrchardBlock>(true);
            orchardBlockLP.ItemChosen += (sender, arg) =>
            {
                VM.CurrOrchardBlock = arg.ChosenItem;
            };
            var obAIO = new AddItemOption()
            {
                CurrItemPropertyName = "CurrOrchardBlock", 
                MenuItem = new MenuItem("Orchard Blocks", () => orchardBlockLP)
            };

            var opLP = new ListingPage<Operator>(true);
            opLP.ItemChosen += (sender, arg) =>
            {
                VM.CurrOperator = arg.ChosenItem;
            };
            var opAIO = new AddItemOption()
            { 
                CurrItemPropertyName = "CurrOperator", 
                MenuItem = new MenuItem("Operators", () => opLP)
            };

            var mItems = new List<NPCBase>
            {
                sprayerAIO,
                obAIO,
                opAIO 
            };

            _itemList.ItemsSource = mItems;
            _itemList.ItemTemplate = new DataTemplate(() =>
            {
                var vCell = new TextCellWithDisclosure();
                vCell.SetBinding(TextCell.TextProperty, "MenuItem.MenuTitle");
                vCell.SetBinding(TextCell.DetailProperty, "DetailText");
                return vCell;
            });
            _itemList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                var currAIO = (AddItemOption)(e.Item);

                Navigation.PushAsync(currAIO.MenuItem.RootPage);
                _itemList.SelectedItem = null;
            };
        }

        Step1VM VM
        {
            get
            {
                return (Step1VM)BindingContext;
            }
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
    }

    public class TextCellWithDisclosure : TextCell
    {

    }

    public class NonScrollingListView : ListView
    {

    }

    public class AddItemOption : NPCBase
    {
        public AddItemOption()
        {
            _s1vm = App.Container.GetInstance<Step1VM>();
            _s1vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == CurrItemPropertyName)
                {
                    RaisePropertyChanged(() => DetailText);
                }
            };
        }

        public MenuItem MenuItem { get; set; }

        public string CurrItemPropertyName { get; set; }

        Step1VM _s1vm;

        public string DetailText
        {
            get
            {
                IDataItem dItem = null;
                switch (CurrItemPropertyName)
                {
                    case "CurrSprayer":
                        dItem = _s1vm.CurrSprayer;
                        break;
                    case "CurrOrchardBlock":
                        dItem = _s1vm.CurrOrchardBlock;
                        break;
                    case "CurrOperator":
                        dItem = _s1vm.CurrOperator;
                        break;
                    default:
                        throw new InvalidCastException();
                }
                return dItem == null ? "none selected" : dItem.Name;
            }
        }
    }
}

