using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zajecia10;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ToTable("Roles");

		builder.HasKey(e=>e.RoleId);

		builder.Property(e=>e.RoleId)
			.HasColumnName("PK_role")
			.IsRequired()
			.HasColumnType("int");

		builder.Property(e=>e.RoleName)
			.HasColumnName("name")
			.IsRequired()
			.HasColumnType("varchar(100)")
			.HasMaxLength(100);
	}
}
