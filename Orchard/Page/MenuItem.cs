using System;
using Xamarin.Forms;

namespace Orchard
{
    public class MenuItem
    {
        public MenuItem(string title, Func<Page> initRootPage)
        {
            MenuTitle = title;

            _initRootPage = initRootPage;
        }

        Func<Page> _initRootPage;

        public string MenuTitle { get; set; }

        NavigationPage _naviPage;

        public NavigationPage NaviPage
        {
            get
            {
                // Delay initiation of a page until requested.
                if (_naviPage == null)
                {
                    var rootPage = _initRootPage();
                    rootPage.Title = MenuTitle;
                    _naviPage = new NavigationPage(rootPage);
                    _initRootPage = null;
                }
                return _naviPage;
            }
        }
    }
}

