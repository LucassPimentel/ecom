using products_api.Endpoints;
using products_api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddPersistence();

var app = builder.Build();

app.MapGroup("/products").MapProductEndpoints();
app.MapGroup("/categories").MapCategoryEndpoints();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
