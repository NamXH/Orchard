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

        public void ChooseSprayerClicked(object sender, EventArgs e)
        {
            var listingPage = new ListingPage<Sprayer>(true);
            listingPage.ItemChosen += (object s, ChosenItemEventArg<Sprayer> arg) =>
            {
                VM.ChosenSprayers.Add(arg.ChosenItem);
                // HACK: force layout update.
                _sprayerList.HeightRequest = 100;
                _sprayerList.HeightRequest = -1;
                Debug.WriteLine("chosen {0}", arg.ChosenItem.Name);
            };
            Navigation.PushAsync(listingPage);
        }

        public void ChooseOrchardBlockClicked(object sender, EventArgs e)
        {
            var listingPage = new ListingPage<OrchardBlock>(true);
            listingPage.ItemChosen += (object s, ChosenItemEventArg<OrchardBlock> arg) =>
            {
                VM.ChosenOrchardBlocks.Add(arg.ChosenItem);
                _orBlockList.HeightRequest = 10;
                _orBlockList.HeightRequest = -1;
                Debug.WriteLine("chosen {0}", arg.ChosenItem.Name);
            };
            Navigation.PushAsync(listingPage);
        }

        public void ChooseOperatorClicked(object sender, EventArgs e)
        {
            var listingPage = new ListingPage<Operator>(true);
            listingPage.ItemChosen += (object s, ChosenItemEventArg<Operator> arg) =>
            {
                VM.ChosenOperators.Add(arg.ChosenItem);
                _opeList.HeightRequest = 10;
                _opeList.HeightRequest = -1;
                Debug.WriteLine("chosen {0}", arg.ChosenItem.Name);
            };
            Navigation.PushAsync(listingPage);
        }
    }

    public class MyPicker : Layout<View>
    {
        public MyPicker()
        {
            IsClippedToBounds = true;
        }

        public static readonly BindableProperty ContentSizeProperty = BindableProperty.Create<MyPicker, Size>(mp => mp.ContentSize, default(Size));

        public Size ContentSize
        {
            get
            {
                return (Size)GetValue(ContentSizeProperty);
            }
            private set
            {
                base.SetValue(ContentSizeProperty, value);
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var i = 0;
            foreach (var c in Children)
            {
                var region = new Rectangle(i * width + x, y, width, height);
                ++i;
                Xamarin.Forms.Layout.LayoutChildIntoBoundingRegion(c, region);
            }

            ContentSize = new Size(i * width, height);

            Debug.WriteLine("lc: {0} {1} {2} {3}", x, y, width, height);
        }
    }
}

