using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{
    public partial class DetailOrchardBlockView : ContentView
    {
        public DetailOrchardBlockView()
        {
            InitializeComponent();

            var currYear = DateTime.Now.Year;
            for (var i = 0; i < 100; ++i)
            {
                _yearPlantedPicker.Items.Add(currYear.ToString());
                currYear--;
            }

            var mu = UnitsNet.Units.LengthUnit.Meter.ToString();

            foreach (var u in Enum.GetNames(typeof(OrchardBlock.LengthUnit)))
            {
                _blockSizeUnit.Items.Add(u);
                _avgTHUnit.Items.Add(u);
                _avgCWUnit.Items.Add(u);
                _avgRLUnit.Items.Add(u);
            }
        }
    }
}

