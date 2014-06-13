using System;
using Xamarin.Forms;
using System.Linq;
using System.ServiceModel.Channels;

namespace Orchard
{
    public static class ViewUtils
    {
        public static void SetupStepView(RelativeLayout rLayout, ScrollView helpSv, StackLayout questionContainer, Command questionTappedCmd)
        {
            var tgr = new TapGestureRecognizer()
            {
                Command = new Command((obj) =>
                {
                    var oldBound = helpSv.Bounds;
                    if (oldBound.Height == 40)
                    {
                        // Need to show.
                        var newBound = new Rectangle(0, rLayout.Height - 200, rLayout.Width, 200);
                        helpSv.LayoutTo(newBound, 250, Easing.CubicInOut);
                    }
                    else
                    {
                        // Need to hide.
                        var newBound = new Rectangle(0, rLayout.Height - 40, rLayout.Width, 40);
                        helpSv.LayoutTo(newBound, 250, Easing.CubicInOut);
                    }

                })
            };
            helpSv.GestureRecognizers.Add(tgr);
            var qControls = questionContainer.Children.Where(x => x is QuestionLayout).Cast<QuestionLayout>();
            var currIdx = 0;
            foreach (var qControl in qControls)
            {
                var questionTgr = new TapGestureRecognizer()
                {
                    Command = questionTappedCmd,
                    CommandParameter = currIdx,
                };
                qControl.GestureRecognizers.Add(questionTgr);
                currIdx++;
            }
        }
    }
}

