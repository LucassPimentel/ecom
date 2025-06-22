using Dapper;
using products_api.DTOs;
using products_api.Entities;
using static products_api.Context.DbContext;

namespace products_api.Endpoints
{
    public static class ProductEndpoints
    {
        public static RouteGroupBuilder MapProductEndpoints(this RouteGroupBuilder group)
        {
            //group.MapGet("/", GetProductsAsync)
            //    .WithSummary("Retrieve all products");

            //group.MapGet("/{id:int}", GetProductByIdAsync)
            //    .WithSummary("Retrieve a product by ID");

            group.MapPost("/new", CreateProductAsync)
                .WithSummary("Create a new product");

            return group;
        }
        //private static IResult GetProductsAsync()
        //{
        //    var product = new Product(1, "Produto 1", "Descrição do Produto 1", 100.00m, 10, 1);
        //    products.Add(product);
        //    return Results.Ok(products);
        //}
        //private static IResult GetProductByIdAsync(int id)
        //{
        //    var product = products.FirstOrDefault(p => p.Id == id);
        //    if (product == null)
        //    {
        //        return Results.NotFound();
        //    }
        //    return Results.Ok(product);
        //}
        private async static Task<IResult> CreateProductAsync(ProductDTO product, GetConnection connectionGetter)
        {
            var sql = @"INSERT INTO Products (Name, CategoryId, Description, Price, Stock, ImageUrl, CreatedAt, UpdatedAt) 
                VALUES (@Name, @CategoryId, @Description, @Price, @Stock, @ImageUrl, @CreatedAt, @UpdatedAt );";

            using var connection = await connectionGetter();
            connection.Open();
            await connection.ExecuteAsync(sql, new
            {
                product.Name,
                product.CategoryId,
                product.Description,
                product.Price,
                product.Stock,
                product.ImageUrl,
                CreatedAt = product.CreatedAt ?? DateTime.Now,
                UpdateAt = product.UpdatedAt ?? DateTime.Now,
            });

            return Results.Created("/products/new", product);
        }
    }

}
