using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Orchard
{
    public class Step1VM : NPCBase
    {
        public Step1VM()
        {
            Common = new StepVMCommon("Step1Questions.txt", "Step1HelpTexts.txt", "Basic Information");

            Q2SelectedIdx = -1;
            Q3AppTime = DateTime.Now;
        }

        public StepVMCommon Common
        {
            get;
            set;
        }

        int _q2SelectedIdx;

        public int Q2SelectedIdx
        {
            get
            {
                return _q2SelectedIdx;
            }
            set
            {
                SetProperty(ref _q2SelectedIdx, value);
            }
        }

        bool _stoogle;

        public bool SToggle
        {
            get
            {
                return _stoogle;
            }
            set
            {
                SetProperty(ref _stoogle, value);
            }
        }

        DateTime _q3AppTime;

        public DateTime Q3AppTime
        {
            get
            {
                return _q3AppTime;
            }
            set
            {
                SetProperty(ref _q3AppTime, value);
            }
        }

        TimeSpan _q4Time;

        public TimeSpan Q4Time
        {
            get
            {
                return _q4Time;
            }
            set
            {
                SetProperty(ref _q4Time, value);
            }
        }
    }
}
