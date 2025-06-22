
using products_api.Entities;

namespace products_api.Endpoints
{
    public static class CategoryEndpoints
    {
        private static readonly List<Category> categories = new List<Category>();
        public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
        {
            //group.MapGet("/", GetCategories)
            //    .WithSummary("Retrieve all categories");

            //group.MapGet("/{id:int}", GetCategoryById)
            //    .WithSummary("Retrieve a category by ID");

            //group.MapPost("/new/{category}", CreateCategory)
            //    .WithSummary("Create a new category");

            return group;
        }

        //private static IResult CreateCategory(Category category)
        //{
        //    categories.Add(category);
        //    return Results.Created($"/categories/{category.CategoryId}", category);
        //}

        //private static IResult GetCategoryById(int id)
        //{
        //    var category = categories.FirstOrDefault(p => p.CategoryId == id);
        //    if (category == null)
        //    {
        //        return Results.NotFound();
        //    }
        //    return Results.Ok(category);
        //}

        //private static IResult GetCategories()
        //{
        //    var category = new Category(1, "Categoria 1");
        //    categories.Add(category);
        //    return Results.Ok(categories);
        //}
    }
}
