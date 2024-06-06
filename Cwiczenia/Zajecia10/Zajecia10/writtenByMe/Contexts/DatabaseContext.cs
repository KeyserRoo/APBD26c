using Microsoft.EntityFrameworkCore;

namespace Zajecia10;

public class DatabaseContext : DbContext
{
	public DbSet<Account> Accounts { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<ShoppingCart> ShoppingCarts { get; set; }
	public DbSet<ProductCategory> ProductCategories { get; set; }

	protected DatabaseContext() { }
	public DatabaseContext(DbContextOptions options) : base(options) { }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		///Annotations
		// base.OnModelCreating(modelBuilder);


		///Configurations
		modelBuilder.Entity<ShoppingCart>()
			.HasKey(e => new { e.AccountId, e.ProductId });

		modelBuilder.Entity<ProductCategory>()
			.HasKey(e => new { e.ProductId, e.CategoryId });

		modelBuilder.ApplyConfiguration(new ProductConfiguration());
		modelBuilder.ApplyConfiguration(new AccountConfiguration());
		modelBuilder.ApplyConfiguration(new CategoryConfiguration());
		modelBuilder.ApplyConfiguration(new RoleConfiguration());
		modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
		modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
	}
}