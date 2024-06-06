using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zajecia10;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
	public void Configure(EntityTypeBuilder<ShoppingCart> builder)
	{
		builder.ToTable("Shopping_Carts");

		builder.Property(e => e.AccountId)
			.HasColumnName("FK_account")
			.IsRequired()
			.HasColumnType("int");

		builder.Property(e => e.ProductId)
			.HasColumnName("FK_product")
			.IsRequired()
			.HasColumnType("int");

		builder.Property(e=>e.Amount)
			.HasColumnName("amount")
			.IsRequired()
			.HasColumnType("int");
	}
}
