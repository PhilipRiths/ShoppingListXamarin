using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class ShoppingListItem
    {
        [Key]
        public int Id { get; set; }

        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public int ShoppingItemId { get; set; }
        public ShoppingItem ShoppingItem { get; set; }
    }
}