using E_Commerece.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Repository.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).IsRequired();
            builder.Property(P => P.Description).IsRequired().HasMaxLength(maxLength:900);
            builder.Property(P=> P.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(P => P.ProductTypeId);
            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P => P.ProductBrandId);


        }
    }
}
