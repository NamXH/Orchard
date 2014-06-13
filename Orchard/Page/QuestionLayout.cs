using System;
using Xamarin.Forms;

namespace Orchard
{
    public class QuestionLayout : StackLayout
    {
        public QuestionLayout()
        {
            _questionLb = new Label();
            this.Children.Add(_questionLb);
        }

        public static readonly BindableProperty QuestionLabelProperty =
            BindableProperty.Create<QuestionLayout, string>(currLayout => currLayout.QuestionLabel, null, BindingMode.OneWay, null,
                (bobj, oldVal, newVal) =>
                {
                    var ql = (QuestionLayout)bobj;
                    ql._questionLb.Text = newVal;
                });

        Label _questionLb;

        public string QuestionLabel
        { 
            get
            {

                return (string)base.GetValue(QuestionLayout.QuestionLabelProperty);
            }
            set
            {
                base.SetValue(QuestionLayout.QuestionLabelProperty, value);
            }
        }

        public StackOrientation Ori
        {
            get
            {
                return base.Orientation;
            }
            set
            {
                base.Orientation = value;
            }
        }
    }
}

