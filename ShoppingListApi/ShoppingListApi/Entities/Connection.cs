using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class Connection
    {
        [Key]
        public string ConnectionId { get; set; }

        public string UserAgent { get; set; }

        public bool Connected { get; set; }
    }
}