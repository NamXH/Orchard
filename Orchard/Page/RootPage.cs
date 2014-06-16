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
            var menuPage = new MenuPage();
            menuPage.MenuItemChanged += OnMenuItemChanged;

            MessagingCenter.Subscribe<Page>(this, "next", sender =>
            {
                Detail = menuPage.GetNextCalcPage(sender.Title);
            });



            Master = new NavigationPage(menuPage) { Title = "Menu" };
            Detail = menuPage.DefaultPage;
        }

        public void OnMenuItemChanged(object sender, MenuItemChangedEventArg e)
        {
            Detail = e.SelectedMenuItem.NaviPage ?? Detail;
            IsPresented = false;
        }
    }
}

