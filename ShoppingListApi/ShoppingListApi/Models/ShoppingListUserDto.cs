namespace ShoppingListApi.Models
{
    public class ShoppingListUserDto
    {
        public int Id { get; set; }

        public int ShoppingListId { get; set; }
        public ShoppingListDto ShoppingList { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}