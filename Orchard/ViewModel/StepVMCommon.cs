using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Orchard
{
    public class StepVMCommon : NPCBase
    {
        public StepVMCommon(string questionListFileName, string helpTextFileName, string defaultHelpText)
        {
            if (questionListFileName != null)
            {
                var qData = Helper.ReadTextDataToLine(questionListFileName);
                QuestionList = new List<string>(qData);
            }

            if (helpTextFileName != null)
            {
                var htData = Helper.ReadTextDataToLine(helpTextFileName);
                _helpTextList = new List<string>(htData);
            }

            _defaultHelpText = defaultHelpText;
            HelpText = _defaultHelpText;
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

        protected List<string> _helpTextList;
        protected string _defaultHelpText;

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
                }));
            }
        }
    }
}

