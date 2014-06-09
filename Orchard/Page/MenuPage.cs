using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Orchard
{
    public class MenuPage : ContentPage
    {
        static readonly List<string> OptionItems = new List<string>();

        public ListView Menu { get; set; }

        public MenuPage()
        {
            Title = "Menu";
            OptionItems.Add("Home");
            OptionItems.Add("Step 1");
            OptionItems.Add("Step 2");
            OptionItems.Add("About");
            OptionItems.Add("Setting");
            OptionItems.Add("Help");

            var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

            var label = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Xamarin.Forms.Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "MENU", 
                }
            };

            Device.OnPlatform(
                iOS: () => ((Xamarin.Forms.Label)label.Content).Font = Font.SystemFontOfSize(NamedSize.Micro),
                Android: () => ((Xamarin.Forms.Label)label.Content).Font = Font.SystemFontOfSize(NamedSize.Medium)
            );

            layout.Children.Add(label);

            Menu = new ListView
            {
                ItemsSource = OptionItems,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent
            };

//            var cell = new DataTemplate(typeof(DarkTextCell));
//            cell.SetBinding(TextCell.TextProperty, "Title");
//            cell.SetBinding(TextCell.DetailProperty, "Count");
//            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
//            cell.SetValue(VisualElement.BackgroundColorProperty, Color.Transparent);
//
//            Menu.ItemTemplate = cell;

            layout.Children.Add(Menu);

            Content = layout;
        }
    }
}

