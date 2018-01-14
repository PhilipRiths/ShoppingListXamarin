using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using ShoppingList.Shared.Models;
using ShoppingList.Shared.Views;
using ShoppingList.Shared.Wrappers;

namespace ShoppingList.Shared.ViewModels
{
    public class SharedListViewModel : BaseViewModel, INavigatingAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public SharedListViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            AddSharedListUserCommand = new DelegateCommand(OnAddSharedListUser);
            DeleteSharedListUserCommand = new DelegateCommand<UserWrapper>(OnDeleteSharedListUser);

            Users = new ObservableCollection<UserWrapper>();
        }

        public DelegateCommand<UserWrapper> DeleteSharedListUserCommand { get; }

        public DelegateCommand AddSharedListUserCommand { get; }

        public ObservableCollection<UserWrapper> Users { get; }

        public GroceryList SelectedGroceryList { get; set; }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters["GroceryList"] is GroceryList selectedGroceryList)
            {
                SelectedGroceryList = selectedGroceryList;

                if (SelectedGroceryList.Users != null)
                {
                    foreach (var user in SelectedGroceryList.Users)
                    {
                        Users.Add(new UserWrapper(user));
                    }
                }
            }

            if (parameters["NewSharedListUser"] is User newSharedListUser)
            {
                Users.Add(new UserWrapper(newSharedListUser));

                // TODO Add to API
                await MockUserDataStore.AddAsync(newSharedListUser);
            }
        }

        private async void OnAddSharedListUser()
        {
            var navigationParameters = new NavigationParameters { { "SelectedGroceryList", SelectedGroceryList } };

            await _navigationService.NavigateAsync(nameof(AddSharedListUserPopup), navigationParameters);
        }

        private async void OnDeleteSharedListUser(UserWrapper selectedUser)
        {
            Users.Remove(selectedUser);
            var user = SelectedGroceryList.Users.Find(u => u.Id == selectedUser.Id);
            SelectedGroceryList.Users.Remove(user);

            await _dialogService.DisplayAlertAsync(
                string.Empty,
                $"You are no longer sharing this list with {selectedUser.FullName}.",
                "OK");

            // TODO Implement API instead of Mock
            await MockShoppingListDataStore.DeleteSharedListUser(SelectedGroceryList.Id, selectedUser);
        }
    }
}