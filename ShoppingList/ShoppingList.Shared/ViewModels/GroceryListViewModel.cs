﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using ShoppingList.Shared.Helpers;
using ShoppingList.Shared.Models;
using ShoppingList.Shared.Views;
using Xamarin.Forms;

namespace ShoppingList.Shared.ViewModels
{
    public class GroceryListViewModel : BaseViewModel, IAsyncInitialization, INavigatingAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public GroceryListViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            NavigateToItemSelected = new DelegateCommand<GroceryList>(OnItemSelected);
            OpenGroceryListDetailCommand = new DelegateCommand<GroceryList>(OnOpenGroceryListDetail);

            Initialization = InitializeAsync();
        }

        public GroceryItem GroceryItem { get; set; }

        public ICommand NavigateToItemSelected { get; }

        public ICommand OpenGroceryListDetailCommand { get; }

        public Task Initialization { get; }

        public ObservableCollection<GroceryList> GroceryLists { get; private set; }

        public GroceryList GroceryList { get; set; }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            // TODO Push changes to API
            if (parameters.Count <= 0) return;
            var groceryList = parameters["GroceryList"] as GroceryList;

            if (groceryList != null && groceryList.Id == 0)
            {
                // Temporary Id solution to not create duplications
                groceryList.Id = GroceryLists.Count + 1;
                GroceryLists.Add(groceryList);
                await MockShoppingListDataStore.AddAsync(groceryList);
            }
            else
            {
                var index = GroceryLists.IndexOf(groceryList);
                GroceryLists.Remove(groceryList);
                GroceryLists.Insert(index, groceryList);
                await MockShoppingListDataStore.UpdateAsync(groceryList);
            }
        }

        private async Task DisplayActionSheet(GroceryList groceryList)
        {
            var navigationParameters = new NavigationParameters { { "GroceryList", groceryList } };

            var editAction = ActionSheetButton.CreateButton(
                "Edit",
                () => _navigationService.NavigateAsync(nameof(GroceryListDetailPopup), navigationParameters));

            var deleteAction = ActionSheetButton.CreateButton(
                "Delete",
                async () =>
                    {
                        var answer = await _dialogService.DisplayAlertAsync(
                                         string.Empty,
                                         "This will be permanently deleted, continue?",
                                         "OK",
                                         "CANCEL");

                        if (answer)
                        {
                            GroceryLists.Remove(groceryList);
                            await MockShoppingListDataStore.DeleteAsync(groceryList.Id);
                        }
                    });

            var sharingAction = ActionSheetButton.CreateButton(
                "Sharing",
                async () => await _navigationService.NavigateAsync(nameof(SharedListPage), navigationParameters));

            var cancelAction = ActionSheetButton.CreateCancelButton("Cancel", () => { });

            await _dialogService.DisplayActionSheetAsync(
                string.Empty,
                editAction,
                deleteAction,
                sharingAction,
                cancelAction);
        }

        private async Task InitializeAsync()
        {
            GroceryLists = new ObservableCollection<GroceryList>(await MockShoppingListDataStore.GetAllAsync());
        }

        private async void OnItemSelected(GroceryList groceryList)
        {
            var navParams = new NavigationParameters { { Title = "ItemList", groceryList } };
            await _navigationService.NavigateAsync(nameof(GroceryItemPage), navParams);
        }

        private async void OnOpenGroceryListDetail(GroceryList groceryList)
        {
            if (groceryList == null)
            {
                var newGroceryListParameter = new NavigationParameters { { "GroceryList", new GroceryList() } };
                await _navigationService.NavigateAsync(nameof(GroceryListDetailPopup), newGroceryListParameter, true);
                return;
            }

            await DisplayActionSheet(groceryList);
        }
    }
}