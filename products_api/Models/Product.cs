using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace products_api.Entities
{
    [Table("Products")]
    public record Product(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock,
        string? ImageUrl,
        int CategoryId,
        Category Category,
        DateTime CreatedAt = default,
        DateTime UpdatedAt = default
    );
    //{
    //    public Product(int id, string name, string description, decimal price, int stock, int categoryId)
    //    {
    //        Id = id;
    //        Name = name;
    //        Description = description;
    //        Price = price;
    //        Stock = stock;
    //        CategoryId = categoryId;
    //    }

    //    public int Id { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public string Description { get; set; } = string.Empty;
    //    public decimal Price { get; set; }
    //    public int Stock { get; set; }
    //    public int CategoryId { get; set; }
    //    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    //    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    //}
}
