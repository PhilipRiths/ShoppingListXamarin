namespace ShoppingList.Shared.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsNotifyGroceryItemAdded { get; set; }

        public bool IsNotifyGroceryItemUpdated { get; set; }

        public bool IsNotifyGroceryItemDeleted { get; set; }
    }
}