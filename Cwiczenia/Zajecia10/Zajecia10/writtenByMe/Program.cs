using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Zajecia10;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddValidatorsFromAssemblyContaining<PostProductModelValidator>();
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/accounts/{accountId:int}", async (int id, IAccountService service) =>
{
	try
	{
		return Results.Ok(await service.GetAccountByIdAsync(id));
	}
	catch (NotFoundException e)
	{
		return Results.NotFound(e.Message);
	}
});

app.MapPost("api/products", async (PostProductRequestModel request, IProductService service, IValidator<PostProductRequestModel> validator) =>
{
	var validate = await validator.ValidateAsync(request);
	if (!validate.IsValid)
	{
		return Results.ValidationProblem(validate.ToDictionary());
	}
	try
	{
		await service.PostProduct(request);
		return Results.NoContent();
	}
	catch (NotFoundException e)
	{
		return Results.NotFound(e.Message);
	}
});

app.Run();