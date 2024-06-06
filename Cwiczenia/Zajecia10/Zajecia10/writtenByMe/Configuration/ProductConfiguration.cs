using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zajecia10;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("Products");

		builder.HasKey(e => e.ProductId);

		builder.Property(e => e.ProductId)
			.HasColumnName("PK_product")
			.IsRequired()
			.HasColumnType("int");

		builder.Property(e => e.ProductName)
			.HasColumnName("name")
			.IsRequired()
			.HasColumnType("varchar(100)")
			.HasMaxLength(100);

		builder.Property(e => e.ProductWeight)
			.IsRequired()
			.HasColumnName("weight")
			.HasColumnType("decimal(5, 2)");

		builder.Property(e => e.ProductWidth)
			.IsRequired()
			.HasColumnName("width")
			.HasColumnType("decimal(5, 2)");

		builder.Property(e => e.ProductHeight)
			.IsRequired()
			.HasColumnName("height")
			.HasColumnType("decimal(5, 2)");

		builder.Property(e => e.ProductDepth)
			.IsRequired()
			.HasColumnName("depth")
			.HasColumnType("decimal(5, 2)");
	}
}