using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(70);
            builder.Property(x => x.Price).HasPrecision(8,2);
            builder.HasOne(x => x.ProductFeature).WithMany(x => x.Products).HasForeignKey(x => x.ProductFeatureId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Product_ProductFeature");
        }
    }
}
