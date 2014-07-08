using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;

namespace Orchard
{
    public partial class Step1Page : ContentPage
    {
        public Step1Page()
        {
            InitializeComponent();

            var vm = new Step1VM();

            BindingContext = vm;

            ViewUtils.SetupStepView(_rLayout, _helpSv, _questionContainer, vm.Common.QuestionTapped);

            foreach (var str in OptimizeModeToPickerIdxCov.Names)
            {
                _opMode.Items.Add(str);
            }

            foreach (var str in RowSprayingModeToPickerIdxCov.Names)
            {
                _rowSprayingMode.Items.Add(str);
            }

//            _addBtn.Command = new Command(() =>
//            {
//                Debug.WriteLine("add");
//                var isproper = _itemList.GetValue(ListView.ItemsSourceProperty);
//                if (object.ReferenceEquals(isproper, vm.ChosenSprayers))
//                {
//                    var listingPage = new ListingPage<Sprayer>(true);
//                    listingPage.ItemChosen += (object s, ChosenItemEventArg<Sprayer> arg) =>
//                    {
//                        VM.ChosenSprayers.Add(arg.ChosenItem);
//                        // HACK: force layout update.
//                        _itemList.HeightRequest = 100;
//                        _itemList.HeightRequest = -1;
//                        Debug.WriteLine("chosen {0}", arg.ChosenItem.Name);
//                    };
//                    Navigation.PushAsync(listingPage);
//                }
//                else if (object.ReferenceEquals(isproper, vm.ChosenOrchardBlocks))
//                {
//                    var listingPage = new ListingPage<OrchardBlock>(true);
//                    listingPage.ItemChosen += (object s, ChosenItemEventArg<OrchardBlock> arg) =>
//                    {
//                        VM.ChosenOrchardBlocks.Add(arg.ChosenItem);
//                        _itemList.HeightRequest = 10;
//                        _itemList.HeightRequest = -1;
//                        Debug.WriteLine("chosen {0}", arg.ChosenItem.Name);
//                    };
//                    Navigation.PushAsync(listingPage);
//                }
//                else if (object.ReferenceEquals(isproper, vm.ChosenOperators))
//                {
//                    var listingPage = new ListingPage<Operator>(true);
//                    listingPage.ItemChosen += (object s, ChosenItemEventArg<Operator> arg) =>
//                    {
//                        VM.ChosenOperators.Add(arg.ChosenItem);
//                        _itemList.HeightRequest = 10;
//                        _itemList.HeightRequest = -1;
//                        Debug.WriteLine("chosen {0}", arg.ChosenItem.Name);
//                    };
//                    Navigation.PushAsync(listingPage);
//                }
//            });

            var a = new List<string>{ "Sprayers", "Orchard Blocks", "Operators" };

            _itemList.ItemsSource = a;
            _itemList.ItemTemplate = new DataTemplate(() =>
            {
                var currCell = new TextCellWithDisclosure();
                currCell.SetBinding(ImageCell.TextProperty, new Binding());

                return currCell;
            });
        }

        Step1VM VM
        {
            get
            {
                return (Step1VM)BindingContext;
            }
        }

        public void ChooseSprayerClicked(object sender, EventArgs e)
        {
            _itemList.SetBinding(ListView.ItemsSourceProperty, "ChosenSprayers");
            //_addBtn.Text = "Add new sprayer";
        }

        public void ChooseOrchardBlockClicked(object sender, EventArgs e)
        {
            _itemList.SetBinding(ListView.ItemsSourceProperty, "ChosenOrchardBlocks");
            //_addBtn.Text = "Add new orchard block";
        }

        public void ChooseOperatorClicked(object sender, EventArgs e)
        {
            _itemList.SetBinding(ListView.ItemsSourceProperty, "ChosenOperators");
            //_addBtn.Text = "Add new operator";
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
}

