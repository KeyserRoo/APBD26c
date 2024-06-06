using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace Zajecia10;
public interface IAccountService
{
	Task<GetAccountresponseModel> GetAccountByIdAsync(int id);
}

public class AccountService(DatabaseContext context) : IAccountService
{
	async Task<GetAccountresponseModel> IAccountService.GetAccountByIdAsync(int id)
	{
		var response = await context.Accounts
			.Join(

				context.Roles,
				acc => acc.AccountRole,
				r => r.RoleId,
				(acc, r) => new { Account = acc, Role = r })

			.Where(e => e.Account.AccountId == id)
			.Select(e => new GetAccountresponseModel
			{
				FirstName = e.Account.AccountFirstName,
				LastName = e.Account.AccountLastName,
				Email = e.Account.AccountEmail,
				Phone = e.Account.AccountPhoneNumber,
				Role = e.Role.RoleName,
			}).FirstOrDefaultAsync();

		if (response is null) throw new NotFoundException($"Account with id:{id} does not exist");

		var purchasedProducts = await context.ShoppingCarts
			.Where(cart => cart.AccountId == id)
			.Join(

				context.Products,
				cart => cart.ProductId,
				product => product.ProductId,
				(cart, product) => new { cart, product })

			.Select(e => new GetAccountresponseModelListElement
			{
				ProductId = e.product.ProductId,
				ProductName = e.product.ProductName,
				Amount = e.cart.Amount
			})
			.ToListAsync();

		response.Cart = new Collection<GetAccountresponseModelListElement>(purchasedProducts);
		
		await context.SaveChangesAsync();
		return response;
	}
}
