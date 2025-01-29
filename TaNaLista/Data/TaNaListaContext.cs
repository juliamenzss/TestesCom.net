using Microsoft.EntityFrameworkCore;
using TaNaLista.Interfaces;
using TaNaLista.Models;
using TaNaLista.Requests;
using TaNaLista.Response;

namespace TaNaLista.Data
{
    public class TaNaListaContext(DbContextOptions<TaNaListaContext> options) : DbContext(options), IUserContext, IProductContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingListProduct>()
                .HasKey(x => new { x.ShoppingListId, x.ProductId });

            modelBuilder.Entity<ShoppingListProduct>()
                .HasOne(x => x.ShoppingList)
                .WithMany(x => x.ShoppingListProducts)
                .HasForeignKey(x => x.ShoppingListId);

            modelBuilder.Entity<ShoppingListProduct>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ShoppingListProducts)
                .HasForeignKey(x => x.ProductId);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<User> Users { get; set; } = default!;
        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ShoppingListProduct> ShoppingListProducts { get; set; } = default!;
    }

}

