using API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMockDB,MockDB>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/minimalZwierzeta", (IMockDB mockDB) =>
{
    return Results.Ok(mockDB.GetAll());
});

app.MapGet("/minimalZwierzeta/{id}", (IMockDB mockDB, int id) =>
{
    var zwierz = mockDB.GetById(id);
    if (zwierz is null) return Results.NotFound();
    else return Results.Ok(zwierz);
});

app.MapPost("/minimalZwierzeta", (IMockDB mockDB, Zwierzontko zwierz) =>
{
    if (mockDB.Add(zwierz)) return Results.Created();
    else return Results.Conflict();
});

app.MapPut("/minimalZwierzeta/{id}", (IMockDB mockDB, int id, Zwierzontko zwierz) =>
{
    mockDB.EditOrAdd(id, zwierz);
    return Results.Ok();
});

app.MapDelete("/minimalZwierzeta/{id}", (IMockDB mockDB, int id) =>
{
    if (mockDB.Delete(id)) return Results.Ok();
    else return Results.NotFound();
});

app.MapGet("/minimalWizyty/{id}", (IMockDB mockDB, int id) =>
{
    var wizyta = mockDB.GetWizytyByZwierzId(id);
    if (wizyta is null) return Results.NotFound();
    else return Results.Ok(wizyta);
});
app.MapPost("/minimalWizyty", (IMockDB mockDB, Wizyta wizyta) =>
{
    if (mockDB.Add(wizyta)) return Results.Created();
    else return Results.Conflict();
});

app.Run();