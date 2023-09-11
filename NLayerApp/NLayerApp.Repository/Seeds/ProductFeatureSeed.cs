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
    internal class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(
                new ProductFeature() { Id = 1, Color = "Red", Height = 100, Width = 100, Weight = 100, ProductId = 1 },
                new ProductFeature() { Id = 2, Color = "Yellow", Height = 200, Width = 200, Weight = 200, ProductId = 2 },
                new ProductFeature() { Id = 3, Color = "Green", Height = 300, Width = 300, Weight = 300, ProductId = 3 },
                new ProductFeature() { Id = 4, Color = "Blue", Height = 400, Width = 400, Weight = 400, ProductId = 4 },
                new ProductFeature() { Id = 5, Color = "Purple", Height = 400, Width = 500, Weight = 500, ProductId = 5 });
        }
    }
}
