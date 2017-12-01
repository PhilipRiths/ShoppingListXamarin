using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;

namespace ShoppingList.Shared.Validation
{
    public class EntryValidatorBehavior : Behavior<Entry>
    {
        private const string EmailRegex =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
            + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            nameof(IsValid),
            typeof(bool),
            typeof(EntryValidatorBehavior),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
            nameof(MaxLength),
            typeof(int),
            typeof(EntryValidatorBehavior),
            0);

        public static readonly BindableProperty CheckEmptyProperty = BindableProperty.Create(
            nameof(CheckEmpty),
            typeof(bool),
            typeof(EntryValidatorBehavior),
            false);

        public static readonly BindableProperty IsEmailProperty = BindableProperty.Create(
            nameof(IsEmail),
            typeof(bool),
            typeof(EntryValidatorBehavior),
            false);

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            nameof(Message),
            typeof(string),
            typeof(EntryValidatorBehavior),
            string.Empty);

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            private set => SetValue(MessageProperty, value);
        }

        public bool CheckEmpty
        {
            get => (bool)GetValue(CheckEmptyProperty);
            private set => SetValue(CheckEmptyProperty, value);
        }

        public bool IsEmail
        {
            get => (bool)GetValue(IsEmailProperty);
            private set => SetValue(IsEmailProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.TextChanged += OnTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (CheckEmpty)
            {
                IsValid = !string.IsNullOrWhiteSpace(e.NewTextValue);
                Message = "Field cannot be empty";

                if (!IsValid)
                {
                    ((Entry)sender).TextColor = Color.Red;
                    return;
                }
            }

            if (IsEmail)
            {
                IsValid = Regex.IsMatch(
                    e.NewTextValue,
                    EmailRegex,
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250));
                Message = "Email is incorrect format";

                if (!IsValid)
                {
                    ((Entry)sender).TextColor = Color.Red;
                    return;
                }
            }

            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > MaxLength)
            {
                IsValid = false;
                Message = "Max length exceeded";

                ((Entry)sender).TextColor = Color.Red;
                return;
            }

            IsValid = true;
            Message = string.Empty;
            ((Entry)sender).TextColor = Color.Default;
        }
    }
}