using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Zajecia10;


///Annotations
// [Table("Accounts")]
// public class Account
// {
// 	[Column("PK_account")]
// 	[Key]
// 	[Required]
// 	public int AccountId { get; set; }


// 	[Column("FK_role")]
// 	[ForeignKey("PK_role")]
// 	[Required]
// 	public int AccountRole { get; set; }


// 	[Column("first_name")]
// 	[MaxLength(50)]
// 	[Required]
// 	public string AccountFirstName { get; set; }


// 	[Column("last_name")]
// 	[MaxLength(50)]
// 	[Required]
// 	public string AccountLastName { get; set; }


// 	[Column("email")]
// 	[MaxLength(80)]
// 	[Required]
// 	public string AccountEmail { get; set; }


// 	[Column("phone")]
// 	[MaxLength(9)]
// 	public string AccountPhoneNumber { get; set; }
// }


///Configuration
public class Account
{
	public int AccountId { get; set; }
	public int AccountRole { get; set; }
	public Role Role { get; set; }
	public string AccountFirstName { get; set; }
	public string AccountLastName { get; set; }
	public string AccountEmail { get; set; }
	public string AccountPhoneNumber { get; set; }
	public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
}