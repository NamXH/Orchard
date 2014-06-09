using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;

namespace Orchard
{
    public class Step1VM : ViewModelBase
    {
        public Step1VM()
        {
            Text = "aaaaaaaaaaaabbbbbbbbbbbbccccccccccc";
        }

        string _text;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        bool _stoogle;
        public bool SToggle
        {
            get {
                return _stoogle;
            }
            set {
                _stoogle = value;
                OnPropertyChanged();
            }
        }

        Command _nextClicked;

        public Command NextClicked
        {
            get
            {
                return _nextClicked ?? (_nextClicked = new Command(() => Debug.WriteLine("next clicked")));
            }
        }
    }
}

