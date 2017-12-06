using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Entities;

namespace ShoppingListApi.Data
{
    public class ShoppingListContext : DbContext
    {
        public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
           : base(options)
        {
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        //public ShoppingListContext()
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = ShoppingListDB; Trusted_Connection = True;");

        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasAlternateKey(u => u.GoogleId); // Unique constraint
        }

        public DbSet<ShoppingList> ShoppingLists { get; set; }

        public DbSet<ShoppingItem> ShoppingItems { get; set; }

        public DbSet<ShoppingListItem> ShoppingListItem { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ShoppingListUser> ShoppingListUser { get; set; }
    }
}