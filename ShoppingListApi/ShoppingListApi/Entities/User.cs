using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class User
    {
        public User()
        {
            ShoppingLists = new List<ShoppingListUser>();
        }

        [Key]
        public Guid Id { get; set; }

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

        public ICollection<ShoppingListUser> ShoppingLists { get; set; }
    }
}