namespace ShoppingList.Shared.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; } = 1;

        public bool InBasket { get; set; }

        public ItemMeasurement Measurement { get; set; }
    }

    public enum ItemMeasurement
    {
        pcs,
        dL,
        L,
        g,
        kg,
        m,
    }
}