namespace ShoppingListApi.Models
{
    public class ShoppingListItemDto
    {
        public int Id { get; set; }

        public int ShoppingListId { get; set; }
        public ShoppingListDto ShoppingList { get; set; }

        public int ShoppingItemId { get; set; }
        public ShoppingItemDto ShoppingItem { get; set; }
    }
}