using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Orchard
{
    public class Step1VM : ViewModelBase
    {
        public Step1VM()
        {
            HelpText = "help text here, long long textThis text is a very boring text and I bet you will not manage to read the whole text since the only period in the text is the one that ends the whole text that is by the way so long and pointless that writing it is just as fun as the text becomes boring since it is so long and pointless and also quite boring since it does not happen a single thing the whole time it is going on which is very long so you will probably not even manage reading to this point which is somewhere halfway and yet you do incredibly as it is since you are reading these words in this long and boring text where nothing happens at all but you will probably stop reading soon as it is just too boring and long and pointless and I notice you are still reading my boring text and wonder how you can withstand it since it is so long and boring plus there is more fun things to do that I would prefer to do rather than reading this soulless text that is really damn boring and now even I am starting to get tired of it so now I am cutting it out.";

            var qData = Helper.ReadTextDataToLine("Step1Questions.txt");

            QuestionList = new List<string>(qData);

            Q2SelectedIdx = -1;
            Q3AppTime = DateTime.Now;
        }

        List<string> _questionList;

        public List<string> QuestionList
        {
            get
            {
                return _questionList;
            }
            set
            {
                SetProperty(ref _questionList, value);
            }
        }

        string _helpText;

        public string HelpText
        {
            get
            {
                return _helpText;
            }
            set
            {
                SetProperty(ref _helpText, value);
            }
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

