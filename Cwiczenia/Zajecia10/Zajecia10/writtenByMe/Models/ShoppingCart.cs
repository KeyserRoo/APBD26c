using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zajecia10;

///Annotations
// [Table("Shopping_Carts")]
// public class ShoppingCart
// {
// 	[Column("FK_account")]
// 	[ForeignKey("PK_account")]
// 	[Required]
// 	public int AccountId { get; set; }


// 	[Column("FK_product")]
// 	[ForeignKey("PK_product")]
// 	[Required]
// 	public int ProductId { get; set; }


// 	[Column("amount")]
// 	[Required]
// 	public int Amount { get; set; }
// }


///Configuration
public class ShoppingCart
{
	public int AccountId { get; set; }
	public Account Account { get; set; }
	public int ProductId { get; set; }
	public Product Product { get; set; }
	public int Amount { get; set; }
}