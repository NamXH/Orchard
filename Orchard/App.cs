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
            var mdp = new MasterDetailPage();
            var menuPage = new MenuPage();

//            menuPage.Menu.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
//                Debug.WriteLine(e.SelectedItem.ToString());
//                var str = e.SelectedItem.ToString();
//                switch(str) {
//                    case "Home":
//                        mdp.Detail = GetIntroPage();
//                        break;
//                    case "Step 1":
//                        mdp.Detail = GetStep1Page();
//                        break;
//                    case "Step 2":
//                        mdp.Detail = new Step2Page();
//                        break;
//                    default:
//                        throw new InvalidOperationException("Invalid menu option");
//                };
//                mdp.IsPresented = false;
//            };

            mdp.Master = menuPage;

            mdp.Detail = new Step1Page();
            return mdp;
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

