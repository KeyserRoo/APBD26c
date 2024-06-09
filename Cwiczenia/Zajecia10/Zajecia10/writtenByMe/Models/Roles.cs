using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zajecia10;

/// Annotations
// [Table("Roles")]
// public class Role
// {
// 	[Column("PK_role")]
// 	[Key]
// 	[Required]
// 	public int PK_role { get; set; }


// 	[Column("name")]
// 	[MaxLength(100)]
// 	[Required]
// 	public string Name { get; set; }
// }


/// Configuration
public class Role
{
	public int RoleId { get; set; }
	public string RoleName { get; set; }
	public IEnumerable<Account> Accounts { get; set; }
}
