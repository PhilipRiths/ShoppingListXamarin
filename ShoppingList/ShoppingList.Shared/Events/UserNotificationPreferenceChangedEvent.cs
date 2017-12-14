using Prism.Events;

using ShoppingList.Shared.Helpers;

namespace ShoppingList.Shared.Events
{
    public class UserNotificationPreferenceChangedEvent : PubSubEvent<UserNotificationPreferenceChangedEventArgs>
    {
    }

    public class UserNotificationPreferenceChangedEventArgs
    {
        public NotificationType NotificationType { get; set; }
    }
}