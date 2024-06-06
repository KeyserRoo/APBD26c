using System.Collections.ObjectModel;

namespace Zajecia10;
public class PostProductRequestModel
{
	public string ProductName { get; set; }
	public int ProductWeight { get; set; }
	public int ProductWidth { get; set; }
	public int ProductHeight { get; set; }
	public int ProductDepth { get; set; }
	public List<int> ProductCategories { get; set; }
}