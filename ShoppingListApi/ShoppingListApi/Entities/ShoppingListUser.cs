using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class ShoppingListUser
    {
        [Key]
        public int Id { get; set; }

        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}