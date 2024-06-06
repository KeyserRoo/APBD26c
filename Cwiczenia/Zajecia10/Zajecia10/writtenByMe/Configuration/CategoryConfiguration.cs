using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zajecia10;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("Categories");

		builder.HasKey(e=>e.CategoryId);

		builder.Property(e=>e.CategoryId)
			.HasColumnName("PK_category")
			.IsRequired()
			.HasColumnType("int");

		builder.Property(e=>e.CategoryName)
			.HasColumnName("name")
			.IsRequired()
			.HasColumnType("varchar(100)")
			.HasMaxLength(100);
	}
}
