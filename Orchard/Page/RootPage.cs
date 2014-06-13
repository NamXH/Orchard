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
            MessagingCenter.Subscribe<Page>(this, "next", sender =>
            {
                var nextPage = _calcPages.Keys.SkipWhile(x => x != sender.Title).Skip(1).First();
                Debug.WriteLine("Next page: {0}", nextPage);
                // BUG: no scrolling when navigate this way.
                Detail = _calcPages[nextPage];
            });

            var pageNames = new string[]
            {
                "Introduction",
                "Step 1",
                "Step 2",
                "Step 3",
                "Step 4",
                "Step 5",
                "Step 6",
                "Step 7",
            };

            _calcPages = new SortedDictionary<string, Page>()
            {
                { pageNames[0], new NavigationPage(new IntroPage() { Title = pageNames[0] }) },
                { pageNames[1], new NavigationPage(new Step1Page() { Title = pageNames[1] }) },
                { pageNames[2], new NavigationPage(new Step2Page() { Title = pageNames[2] }) },
                { pageNames[3], new NavigationPage(new ContentPage(){ Title = pageNames[3] }) },
                { pageNames[4], new NavigationPage(new ContentPage(){ Title = pageNames[4] }) },
                { pageNames[5], new NavigationPage(new ContentPage(){ Title = pageNames[5] }) },
                { pageNames[6], new NavigationPage(new ContentPage(){ Title = pageNames[6] }) },
                { pageNames[7], new NavigationPage(new ContentPage(){ Title = pageNames[7] }) },
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

            Detail = _calcPages["Step 1"];
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
    }
}

