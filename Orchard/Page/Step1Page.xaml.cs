using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;

namespace Orchard
{
    public partial class Step1Page : ContentPage
    {
        public Step1Page()
        {
            InitializeComponent();

            BindingContext = new Step1VM();

            var tgr = new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    if (_showingHelp)
                    {
                        // Need to hide.
                        var newBound = new Rectangle(0, _rLayout.Height - 40, _rLayout.Width, 40);
                        _helpSv.LayoutTo(newBound, 250, Easing.CubicInOut);
                    }
                    else
                    {
                        // Need to show.
                        var newBound = new Rectangle(0, _rLayout.Height - 200, _rLayout.Width, 200);
                        _helpSv.LayoutTo(newBound, 250, Easing.CubicInOut);
                    }
                    _showingHelp = !_showingHelp;
                })
            };
            _helpSv.GestureRecognizers.Add(tgr);

            var qControls = _questionContainer.Children.Where(x => x is QuestionLayout).Cast<QuestionLayout>();
            var currIdx = 0;
            foreach (var qControl in qControls)
            {
                var questionTgr = new TapGestureRecognizer()
                {
                    Command = ((Step1VM)BindingContext).QuestionTapped,
                    CommandParameter = currIdx,
                };
                qControl.GestureRecognizers.Add(questionTgr);
                currIdx++;
            }
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }

        bool _showingHelp;
    }
}

