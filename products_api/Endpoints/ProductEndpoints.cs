using Dapper;
using products_api.DTOs;
using static products_api.Context.DbContext;

namespace products_api.Endpoints
{
    public static class ProductEndpoints
    {
        public static RouteGroupBuilder MapProductEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetProductsAsync)
                .WithSummary("Retrieve all products");

            group.MapGet("/{id:int}", GetProductByIdAsync)
                .WithSummary("Retrieve a product by ID");

            group.MapPost("/new", CreateProductAsync)
                .WithSummary("Create a new product")
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> GetProductsAsync(GetConnection connectionGetter)
        {
            using var connection = await connectionGetter();

            var products = await connection.QueryAsync<GetProductDTO>(@"
            SELECT [Id]      
            ,[CategoryId]      
            ,[Name]      
            ,[Description]      
            ,[Price]      
            ,[Stock]      
            ,[ImageUrl]      
            ,[CreatedAt]      
            ,[UpdatedAt]  
            FROM [dbo].[Products]");

            return Results.Ok(products);
        }

        private static async Task<IResult> GetProductByIdAsync(int Id, GetConnection connectionGetter)
        {
            using var connection = await connectionGetter();

            var product = await connection.QueryFirstOrDefaultAsync<GetProductDTO>(@"
            SELECT [Id]      
            ,[CategoryId]      
            ,[Name]      
            ,[Description]      
            ,[Price]      
            ,[Stock]      
            ,[ImageUrl]      
            ,[CreatedAt]      
            ,[UpdatedAt]  
            FROM [dbo].[Products]  
            WHERE Id = @Id", new { Id });

            return product is not null ? Results.Ok(product) : Results.NotFound();
        }

        private async static Task<IResult> CreateProductAsync(PostProductDTO product, GetConnection connectionGetter)
        {
            var sql = @"
                INSERT INTO Products (Name, CategoryId, Description, Price, Stock, ImageUrl, CreatedAt) 
                VALUES (@Name, @CategoryId, @Description, @Price, @Stock, @ImageUrl, GETDATE());";

            using var connection = await connectionGetter();
            await connection.ExecuteAsync(sql, new
            {
                product.Name,
                product.CategoryId,
                product.Description,
                product.Price,
                product.Stock,
                product.ImageUrl
            });

            return Results.Created("/products/new", product);
        }
    }

}
