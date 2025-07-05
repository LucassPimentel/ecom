
using Dapper;
using products_api.DTOs;
using products_api.Entities;
using static products_api.Context.DbContext;

namespace products_api.Endpoints
{
    public static class CategoryEndpoints
    {
        public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetCategoriesAsync)
                .WithSummary("Obter todas as categorias.");

            group.MapGet("/{id:int}", GetCategoryByIdAsync)
                .WithSummary("Obter categoria por ID.");

            group.MapPost("/new/{category}", CreateCategoryAsync)
                .WithSummary("Criar nova categoria.");

            return group;
        }

        private static async Task<IResult> CreateCategoryAsync(GetConnection connectionGetter, PostCategoryDTO postCategoryDto)
        {
            var sql = @"INSERT INTO [dbo].[Categories] ([Title], [CreatedAt])
                        VALUES (@Title, GETDATE())";

            using var connection = await connectionGetter();
            await connection.ExecuteAsync(sql, new
            {
                postCategoryDto.Title,
            });

            return Results.Created("/categories/new", postCategoryDto);
        }

        private static async Task<IResult> GetCategoriesAsync(GetConnection connectionGetter)
        {
            var query = @"
                SELECT [Id], 
                [Title], 
                [CreatedAt],
                [UpdatedAt]
                FROM [dbo].[Categories]";

            using var connection = await connectionGetter();
            var categories = await connection.QueryAsync<GetCategoryDTO>(query);

            return Results.Ok(categories);
        }
        private static async Task<IResult> GetCategoryByIdAsync(int id, GetConnection connectionGetter)
        {
            var queryById = @"
                SELECT [Id], 
                [Title], 
                [CreatedAt],
                [UpdatedAt]
                FROM [dbo].[Categories]
                WHERE Id = @id";

            using var connection = await connectionGetter();
            var category = await connection.QueryFirstOrDefaultAsync<GetCategoryDTO>(queryById, new { id });

            return category is not null ? Results.Ok(category) : Results.NotFound();
        }
    }
}
