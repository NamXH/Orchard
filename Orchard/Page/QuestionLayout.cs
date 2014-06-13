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

        Label _questionLb;

        public string QuestionLabel
        { 
            get
            {
                return _questionLb.Text;
            }
            set
            {
                _questionLb.Text = value;
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

