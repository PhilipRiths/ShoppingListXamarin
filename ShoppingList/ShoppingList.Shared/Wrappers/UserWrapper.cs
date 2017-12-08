using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Prism.Mvvm;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.Wrappers
{
    public class UserWrapper : BindableBase
    {
        public UserWrapper(User user)
        {
            Model = user;
        }

        public User Model { get; }

        public int Id => Model.Id;

        public string FirstName
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
                RaisePropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get => GetValue<string>();
            set
            {
                SetValue(value);
                RaisePropertyChanged(nameof(FullName));
            }
        }

        public string Email
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public bool IsNotifyGroceryItemAdded
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public bool IsNotifyGroceryItemUpdated
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public bool IsNotifyGroceryItemDeleted
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public string FullName => $"{FirstName} {LastName}";

        private TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(User).GetProperty(propertyName).GetValue(Model);
        }

        private void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            var oldValue = GetValue<TValue>(propertyName);

            if (EqualityComparer<TValue>.Default.Equals(oldValue, value)) return;

            typeof(User).GetProperty(propertyName).SetValue(Model, value);
            RaisePropertyChanged(propertyName);
        }
    }
}