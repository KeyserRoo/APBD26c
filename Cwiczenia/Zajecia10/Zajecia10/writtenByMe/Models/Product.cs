namespace Zajecia10;


///Annotations
// [Table("Products")]
// public class Product
// {
//     [Column("PK_product")]
//     [Key]
//     [Required]
//     public int ProductId { get; set; }


//     [Column("name")]
//     [MaxLength(100)]
//     [Required]
//     public string ProductName { get; set; }


//     [Column("weight")]
//     [Required]
//     public decimal ProductWeight { get; set; }


//     [Column("width")]
//     [Required]
//     public decimal ProductWidth { get; set; }


//     [Column("height")]
//     [Required]
//     public decimal ProductHeight { get; set; }


//     [Column("depth")]
//     [Required]
//     public decimal ProductDepth { get; set; }
// }


///Configuration
public class Product
{
	public int ProductId { get; set; }
	public string ProductName { get; set; }
	public decimal ProductWeight { get; set; }
	public decimal ProductWidth { get; set; }
	public decimal ProductHeight { get; set; }
	public decimal ProductDepth { get; set; }
	public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
}