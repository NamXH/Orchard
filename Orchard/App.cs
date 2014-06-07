using System;
using Xamarin.Forms;
using PCLStorage;

namespace Orchard
{
    public class App
    {
        public static Page GetMainPage()
        {	
            return new ContentPage
            { 
                Content = new Label
                {
                    Text = "Crop-Adapted Spray Calculator",
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                },
            };
        }

        public static Page GetIntroPage()
        {   
            var f = FileSystem.Current.GetFileFromPathAsync("Data/Intro.txt").Result;
            var str = f.ReadAllTextAsync().Result;
           
            return new ContentPage
            { 
                Content = new Label
                {
                    Text = str,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                },
            };
        }
    }
}

