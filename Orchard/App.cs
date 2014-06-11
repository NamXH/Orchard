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
    }
}

