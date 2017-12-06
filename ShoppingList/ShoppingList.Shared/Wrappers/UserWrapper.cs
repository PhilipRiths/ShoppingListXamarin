using System.Runtime.CompilerServices;

using Prism.Mvvm;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.Wrappers
{
    public class UserWrapper : BindableBase
    {
        private readonly User _user;

        public UserWrapper(User user)
        {
            _user = user;
        }

        public int Id => _user.Id;

        public string FirstName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string LastName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Email
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string FullName
        {
            get
            {
                var fullName = $"{FirstName} {LastName}";
                RaisePropertyChanged();
                return fullName;
            }
        }

        private TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(User).GetProperty(propertyName).GetValue(_user);
        }

        private void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(User).GetProperty(propertyName).SetValue(_user, value);
            RaisePropertyChanged(propertyName);
        }
    }
}