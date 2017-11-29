﻿using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

using ShoppingList.Shared.Views;

namespace ShoppingList.Shared.ViewModels
{
    public class ShoppingListViewModel
    {
        private readonly INavigationService _navigationService;

        public ShoppingListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddShoppingListCommand = new DelegateCommand(OnAddShoppingList);
        }

        public ICommand AddShoppingListCommand { get; }

        private void OnAddShoppingList()
        {
            _navigationService.NavigateAsync(nameof(ShoppingListDetailPage), null, true);
        }
    }
}