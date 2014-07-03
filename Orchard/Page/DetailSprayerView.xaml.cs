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

            foreach (var u in EnumToPickerIdxConverter.SprayerCatNames)
            {
                _sprayerCat.Items.Add(u);
            }

            foreach (var u in EnumToPickerIdxConverter.VolumeUnitNames)
            {
                _tankCapUnit.Items.Add(u);
            }
        }
    }
}

