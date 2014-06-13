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
            var qData = Helper.ReadTextDataToLine("Step1Questions.txt");

            QuestionList = new List<string>(qData);

            var htData = Helper.ReadTextDataToLine("Step1HelpTexts.txt");
            _helpTextList = new List<string>(htData);
            _defaultHelpText = "default help text";

            HelpText = _defaultHelpText;

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

        List<string> _helpTextList;
        string _defaultHelpText;

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

        Command _questionTapped;

        public Command QuestionTapped
        {
            get
            {
                return _questionTapped ?? (_questionTapped = new Command((para) =>
                {
                    var idx = (int)para;
                    if (string.CompareOrdinal(HelpText, _helpTextList[idx]) == 0)
                    {
                        // Tapped again on the same question, revert to default help text.
                        HelpText = _defaultHelpText;
                    }
                    else
                    {
                        // Tapped on another question.
                        HelpText = _helpTextList[idx];
                    }
                    Debug.WriteLine("Question number {0}", idx);
                }));
            }
        }
    }
}

