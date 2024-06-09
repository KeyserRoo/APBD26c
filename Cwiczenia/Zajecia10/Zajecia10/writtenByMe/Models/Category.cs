namespace Zajecia10;
public class Category
{
	public int CategoryId { get; set; }
	public string CategoryName { get; set; }
	public IEnumerable<ProductCategory> ProductCategories { get; set; }
}
