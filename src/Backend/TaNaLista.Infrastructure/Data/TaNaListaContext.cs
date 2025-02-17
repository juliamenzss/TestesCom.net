using Microsoft.EntityFrameworkCore;
using TaNaLista.Domain.Interfaces;
using TaNaLista.Domain.Models;

namespace TaNaLista.Infrastructure.Data
{
    public class TaNaListaContext : DbContext, IUserContext, IProductContext
    {
        public TaNaListaContext(DbContextOptions<TaNaListaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define a tabela de junção entre ShoppingList e Product (sem necessidade de um DbSet explicitamente)
            modelBuilder.Entity<ShoppingListProduct>()
                .HasKey(x => new { x.ShoppingListId, x.ProductId });

            modelBuilder.Entity<ShoppingListProduct>()
                .HasOne(x => x.ShoppingList)
                .WithMany(x => x.ShoppingListProduct)
                .HasForeignKey(x => x.ShoppingListId)
                .OnDelete(DeleteBehavior.Cascade);  // Defina o comportamento de exclusão, se necessário

            modelBuilder.Entity<ShoppingListProduct>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ShoppingListProduct)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);  // Defina o comportamento de exclusão, se necessário

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
    }
}
