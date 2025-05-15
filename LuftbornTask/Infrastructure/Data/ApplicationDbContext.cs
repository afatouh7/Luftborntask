using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer ;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                 new Product
                 {
                     Id = new Guid("11111111-1111-1111-1111-111111111111"),
                     Name = "Laptop",
                     Description = "High performance laptop",
                     Price = 999.99m,
                     Quantity = 10,
                     CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                 },
                 new Product
                 {
                     Id = new Guid("22222222-2222-2222-2222-222222222222"),
                     Name = "Smartphone",
                     Description = "Latest model smartphone",
                     Price = 699.99m,
                     Quantity = 15,
                     CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                 }
             );
        }
    }
}