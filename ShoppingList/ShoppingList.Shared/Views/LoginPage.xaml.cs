using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}