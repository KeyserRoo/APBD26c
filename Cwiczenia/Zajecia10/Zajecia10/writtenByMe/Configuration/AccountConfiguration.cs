using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zajecia10;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
	public void Configure(EntityTypeBuilder<Account> builder)
	{
		builder.ToTable("Accounts");

		builder.HasKey(e => e.AccountId);

		builder.Property(e => e.AccountId)
			.HasColumnName("PK_account")
			.IsRequired()
			.HasColumnType("int");

		builder.Property(e => e.AccountRole)
			.HasColumnName("FK_role")
			.IsRequired()
			.HasColumnType("int");

		builder.Property(e => e.AccountFirstName)
			.HasColumnName("first_name")
			.IsRequired()
			.HasColumnType("varchar(50)")
			.HasMaxLength(50);

		builder.Property(e => e.AccountLastName)
			.HasColumnName("last_name")
			.IsRequired()
			.HasColumnType("varchar(50)")
			.HasMaxLength(50);

		builder.Property(e => e.AccountEmail)
			.HasColumnName("email")
			.IsRequired()
			.HasColumnType("varchar(80)")
			.HasMaxLength(80);

		builder.Property(e => e.AccountPhoneNumber)
			.HasColumnName("phone")
			.HasColumnType("varchar(9)")
			.HasMaxLength(9);
	}
}