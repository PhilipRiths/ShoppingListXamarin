using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class FavoriteItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}