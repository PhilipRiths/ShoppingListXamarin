using System;
using System.Collections.Generic;
using System.Text;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared
{
   public class MainPageTabbed : TabbedPage
   {
       public MainPageTabbed()
       {
           Page userPage, displayListPage, loginPage = null;

           switch (Device.RuntimePlatform)
           {
               case Device.iOS:
                    userPage = new NavigationPage(new UserPage())
                    {Title = "Users"};
                    displayListPage = new NavigationPage(new ListPage())
                    {Title = "Shoppinglist"};
                        loginPage = new NavigationPage(new LoginPage())
                        {Title = "Login page"};
                   break;

                default:
                    loginPage = new LoginPage()
                        { Title = "Login page" };
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
           Children.Add(loginPage);
            Children.Add(userPage);
            Children.Add(displayListPage);


           Title = "ShoppingList";
       }
     
     

      
   }
}
