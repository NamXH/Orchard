using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using SimpleInjector;

namespace Orchard
{
    public class App
    {
        static App()
        {
            Container = new Container();
            Container.RegisterSingle(() => new Step1VM());
        }

        public static Container Container { get; private set; }

        public static Page GetMainPage()
        {	
            var rootPage = new RootPage();

            return rootPage;
        }
    }
}

