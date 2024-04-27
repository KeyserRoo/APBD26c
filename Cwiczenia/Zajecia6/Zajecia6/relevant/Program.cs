using FluentValidation;
using FluentValidation.AspNetCore;
using Zajecia6;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAnimalRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EditAnimalRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.RegisterAnimalsEndpoints();
app.UseHttpsRedirection();

app.Run();