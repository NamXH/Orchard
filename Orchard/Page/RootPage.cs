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
            _calcPages = new Dictionary<string, Page>()
            {
                { "Intro", null },
                { "Step 1", new Step1Page() },
                { "Step 2", new Step2Page() },
                { "Step 3", null },
                { "Step 4", null },
                { "Step 5", null },
                { "Step 6", null },
                { "Step 7", null },
            };

            _appPages = new Dictionary<string, Page>()
            {
                { "About", null },
                { "Settings", null },
                { "Help", null },
            };

            var menuPage = new MenuPage(_calcPages.Keys.ToList(), _appPages.Keys.ToList());

            menuPage.MenuItemChanged += OnMenuItemChanged;

            Master = menuPage;

            Detail = new Step1Page();
        }

        Dictionary<string, Page> _calcPages;

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

