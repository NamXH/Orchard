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
            Container.RegisterSingle(() => new Step2VM());
            Container.RegisterSingle(() => new Step3VM());
            Container.RegisterSingle(() => new Step4VM());
        }

        public static Container Container { get; private set; }

        public static Page GetMainPage()
        {	
            var rootPage = new RootPage();

            return rootPage;
        }
    }
}

