using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShoppingList.Shared
{
   public class MainPageTabbed : TabbedPage
   {
       public MainPageTabbed()
       {
           Page userPage, displayListPage = null;

           switch (Device.RuntimePlatform)
           {
               case Device.iOS:
                    userPage = new NavigationPage(new UserPage())
                    {Title = "Users"};
                    displayListPage = new NavigationPage(new ListPage())
                    {Title = "Shoppinglist"};
                   break;

                default:
                    userPage = new UserPage()
                    {
                        Title = "User"
                    };
                    displayListPage = new ListPage()
                    {
                        Title = "Shoppinglist"
                    };
                    break;
                       
            }
            Children.Add(userPage);
            Children.Add(displayListPage);

           Title = Children[0].Title;
       }
     
     

      
   }
}
