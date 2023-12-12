using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configurations
{
	public class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(P => P.Status)
				   .HasConversion(S => S.ToString(), S => (OrderStatus) Enum.Parse(typeof(OrderStatus), S));

			builder.Property(P => P.SubTotal)
				   .HasColumnType("decimal(18,2)");

			builder.OwnsOne(P => P.ShippingAddress, Sh => Sh.WithOwner());

			builder.HasOne(P=>P.DeliveryMethod)
				   .WithMany()
				   .OnDelete(DeleteBehavior.NoAction);
			       
		}
	}
}
