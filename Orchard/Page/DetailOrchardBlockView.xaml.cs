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

            foreach (var u in YearToPickerIdxConverter.YearNames)
            {
                _yearPlantedPicker.Items.Add(u);
            }

            foreach (var u in AreaUnitToPickerIdxCov.Names)
            {
                _blockSizeUnit.Items.Add(u);
            }

            foreach (var u in LengthUnitToPickerIdxCov.Names)
            {
                _avgTHUnit.Items.Add(u);
                _avgCWUnit.Items.Add(u);
                _avgRLUnit.Items.Add(u);
            }
        }
    }
}

