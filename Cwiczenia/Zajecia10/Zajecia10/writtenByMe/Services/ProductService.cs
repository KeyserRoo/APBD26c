using Microsoft.EntityFrameworkCore;

namespace Zajecia10;

public interface IProductService
{
	Task PostProduct(PostProductRequestModel request);
}

public class ProductService(DatabaseContext context) : IProductService
{
	async Task IProductService.PostProduct(PostProductRequestModel request)
	{
		var product = new Product
        {
            ProductName = request.ProductName,
            ProductDepth = request.ProductDepth,
            ProductHeight = request.ProductHeight,
            ProductWeight = request.ProductWeight,
            ProductWidth = request.ProductWidth,
        };

        var categories = await context.Categories.Where(e => request.ProductCategories.Any(e2 => e.CategoryId == e2)).Select(e => e.CategoryId).ToListAsync();
        
        request.ProductCategories.ForEach(e =>
        {
            if (!categories.Contains(e))
            {
                throw new NotFoundException($"Category with id:{e} does not exist");
            }
        });

        var productCategories = request.ProductCategories.Select(e => new ProductCategory
        {
            ProductId = product.ProductId,
            CategoryId = e
        });

        await context.ProductCategories.AddRangeAsync(productCategories);
	}
}