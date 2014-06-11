using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Orchard
{
    public class App
    {
        public static Page GetMainPage()
        {	
            var rootPage = new RootPage();

            return rootPage;
        }

        public static Page GetIntroPage()
        {   
            var str = ReadTextData("Intro.txt");
           
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

        public static Page GetStep1Page()
        {
            var ret = new Step1Page();
            return ret;
        }

        public static string ReadTextData(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var resName = string.Format("Orchard.Data.{0}", filename);

            using (var stream = assembly.GetManifestResourceStream(resName))
            {
                using (var sr = new StreamReader(stream))
                {
                    var str = sr.ReadToEnd();
                    return str;
                }
            }
        }
    }
}

