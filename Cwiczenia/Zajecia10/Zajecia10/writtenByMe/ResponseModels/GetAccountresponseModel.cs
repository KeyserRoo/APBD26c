using System.Collections.ObjectModel;

namespace Zajecia10;

public class GetAccountresponseModel
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Role { get; set; }
	public Collection<GetAccountresponseModelListElement> Cart { get; set; }
}

public class GetAccountresponseModelListElement
{
	public int ProductId { get; set; }
	public string ProductName { get; set; }
	public int Amount { get; set; }
}