using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{
    public partial class DetailSprayerView : ContentView
    {
        public DetailSprayerView()
        {
            InitializeComponent();

            foreach (var u in SprayerCatToPickerIdxCov.Names)
            {
                _sprayerCat.Items.Add(u);
            }

            foreach (var u in VolumeUnitToPickerIdxCov.Names)
            {
                _tankCapUnit.Items.Add(u);
            }
        }
    }
}

