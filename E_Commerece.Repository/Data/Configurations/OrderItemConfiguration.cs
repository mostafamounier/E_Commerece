using E_Commerece.Core.Models.Order_Aggreation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Repository.Data.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id); // ← ناقص

            builder.OwnsOne(o => o.Product, SA => SA.WithOwner());

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
