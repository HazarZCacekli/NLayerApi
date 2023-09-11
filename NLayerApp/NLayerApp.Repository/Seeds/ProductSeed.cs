using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product() { Id = 1, Name = "Telefon 1", Price = 11999, Stock = 100, ProductFeatureId = 1 },
                new Product() { Id = 2, Name = "Telefon 2", Price = 12999, Stock = 200, ProductFeatureId = 2 },
                new Product() { Id = 3, Name = "Telefon 3", Price = 13999, Stock = 300, ProductFeatureId = 3 },
                new Product() { Id = 4, Name = "Telefon 4", Price = 14999, Stock = 400, ProductFeatureId = 4 },
                new Product() { Id = 5, Name = "Telefon 5", Price = 15999, Stock = 500, ProductFeatureId = 5 });
        }
    }
}
