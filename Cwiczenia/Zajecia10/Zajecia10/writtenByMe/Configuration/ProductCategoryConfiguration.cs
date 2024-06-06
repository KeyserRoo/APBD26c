using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zajecia10;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
	public void Configure(EntityTypeBuilder<ProductCategory> builder)
	{
		builder.ToTable("Products_Categories");

		builder.Property(e => e.ProductId)
			.HasColumnName("FK_product")
			.IsRequired()
			.HasColumnType("int");

		builder.Property(e => e.CategoryId)
			.HasColumnName("FK_category")
			.IsRequired()
			.HasColumnType("int");
	}
}
