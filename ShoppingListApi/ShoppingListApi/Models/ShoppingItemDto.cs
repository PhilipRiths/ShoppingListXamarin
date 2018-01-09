namespace ShoppingListApi.Models
{
    public class ShoppingItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsBought { get; set; }
    }
}