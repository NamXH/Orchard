using System;
using Xamarin.Forms;

namespace Orchard
{
    public class QuestionLayout : Grid
    {
        public QuestionLayout()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;

            _questionLb = new Label(){ Font = Font.SystemFontOfSize(NamedSize.Small), VerticalOptions = LayoutOptions.Center }; 
            this.Children.Add(_questionLb);
            Orientation = StackOrientation.Horizontal;
        }

        protected override void OnAdded(View view)
        {
            base.OnAdded(view);
            switch (Orientation)
            {
                case StackOrientation.Horizontal:
                    {
                        if (view != _questionLb)
                        {
                            Grid.SetColumn(view, 1);
                        }
                    }
                    break;
                case StackOrientation.Vertical:
                    {
                        if (view != _questionLb)
                        {
                            Grid.SetRow(view, 1);
                        }
                    }
                    break;
            }
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

        StackOrientation _ori;

        public StackOrientation Orientation
        {
            get{ return _ori; }
            set
            {
                _ori = value;
                // Clear old layout.
                RowDefinitions.Clear();
                ColumnDefinitions.Clear();
                _questionLb.WidthRequest = -1;
                // Setup new layout.
                if (_ori == StackOrientation.Horizontal)
                {
                    _questionLb.WidthRequest = 100;
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = new GridLength(100) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    };
                    foreach (var cView in Children)
                    {
                        if (cView != _questionLb)
                        {
                            Grid.SetColumn(cView, 1);
                        }
                    }
                }
                else
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    };
                    RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition{ Height = GridLength.Auto },
                        new RowDefinition{ Height = GridLength.Auto },
                    };
                    // HACK: label inside a grid would not wrap. Set width manually fixes it.
                    _questionLb.WidthRequest = 200;
                    foreach (var cView in Children)
                    {
                        if (cView != _questionLb)
                        {
                            Grid.SetRow(cView, 1);
                        }
                    }
                }
            }
        }
    }
}

