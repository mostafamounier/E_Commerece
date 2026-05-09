using E_Commerece.Core.Models.Order_Aggreation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Repository.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress,SA=>SA.WithOwner());
            builder.Property(o => o.Status)
                .HasConversion(
                    os => os.ToString(),
                    os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os)
                );
            builder.HasMany(o => o.Items).WithOne().HasForeignKey("OrderId").OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasOne(O => O.deliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

       
        }
    }
}
