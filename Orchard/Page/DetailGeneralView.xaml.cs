using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{
    public partial class DetailGeneralView : ContentView
    {
        public DetailGeneralView()
        {
            InitializeComponent();

        }

        public StackLayout MainStackLayout
        {
            get { return _sl; }
        }

        public void SetupHandlers(EventHandler imgBtnHandler, EventHandler delBtnHandler)
        {
            _imgButton.Clicked += imgBtnHandler;
            _delBtn.Clicked += delBtnHandler;
        }
    }
}

