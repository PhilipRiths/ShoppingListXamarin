using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Entities
{
    public class FavoriteItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
