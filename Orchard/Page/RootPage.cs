using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Orchard
{
    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            _nextPageCmd = new Command(currTitle =>
            {
                var nextPage = _calcPages.Keys.SkipWhile(x => x!= (string)currTitle).Skip(1).First();
                Debug.WriteLine("Next page: {0}", nextPage);
                Detail = _calcPages[nextPage];
            });

            _calcPages = new SortedDictionary<string, Page>()
            {
                { "Introduction", new NavigationPage(new IntroPage(){ NextPageCmd = _nextPageCmd }) },
                { "Step 1", new NavigationPage(new Step1Page()) },
                { "Step 2", new NavigationPage(new Step2Page()) },
                { "Step 3", new NavigationPage(new ContentPage()) },
                { "Step 4", new NavigationPage(new ContentPage()) },
                { "Step 5", new NavigationPage(new ContentPage()) },
                { "Step 6", new NavigationPage(new ContentPage()) },
                { "Step 7", new NavigationPage(new ContentPage()) },
            };

            _appPages = new Dictionary<string, Page>()
            {
                { "About", new NavigationPage(new ContentPage()) },
                { "Settings", new NavigationPage(new ContentPage()) },
                { "Help", new NavigationPage(new ContentPage()) },
            };

            var menuPage = new MenuPage(_calcPages.Keys.ToList(), _appPages.Keys.ToList());

            menuPage.MenuItemChanged += OnMenuItemChanged;

            Master = new NavigationPage(menuPage) { Title = "Menu" };

            Detail = _calcPages["Introduction"];
        }

        SortedDictionary<string, Page> _calcPages;

        Dictionary<string, Page> _appPages;

        public void OnMenuItemChanged(object sender, MenuItemChangedEventArg e)
        {
            Debug.WriteLine(e.PageName);
            if (_calcPages.ContainsKey(e.PageName))
            {
                Detail = _calcPages[e.PageName] ?? Detail;
            }

            IsPresented = false;
        }

        Command _nextPageCmd;
    }
}

