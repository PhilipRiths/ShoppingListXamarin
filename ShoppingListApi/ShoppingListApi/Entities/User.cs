using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Mail { get; set; }

        [Required]
        public string GoogleId { get; set; }

        public ICollection<ShoppingListUser> ShoppingLists { get; set; } = new List<ShoppingListUser>();

        public ICollection<FavoriteItem> FavoriteItems { get; set; } = new List<FavoriteItem>();
    }
}