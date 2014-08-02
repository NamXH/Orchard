using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Orchard
{
    public partial class Step3Page : ContentPage
    {
        public Step3Page()
        {
            InitializeComponent();

            foreach (var str in PressureUnitToPickerIdxCov.Names)
            {
                _pressureUnit.Items.Add(str);
            }

            foreach (var str in SpeedUnitToPickerIdxCov.Names)
            {
                _speedUnit.Items.Add(str);
            }

            foreach (var str in NozzleUnitToPickerIdxCov.Names)
            {
                _nozzleUnit.Items.Add(str);
            }

            BindingContext = new Step3VM();

            VM.PropertyChanged += (sender, e) =>
            {
                if (string.CompareOrdinal(e.PropertyName, "ActiveNozzleNum") == 0)
                {
                    // HACK: force grid to redraw layout.
                    _grid.Children.Remove(_nList);

                    var dt = Task.Delay(100);
                    dt.ContinueWith(t => Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        _grid.Children.Add(_nList);
                    }));
                    dt.Start();
                }
            };
        }

        Step3VM VM
        {
            get
            {
                return (Step3VM)BindingContext;
            }
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
    }

    public class NozzleListView : ListView
    {

    }
}

