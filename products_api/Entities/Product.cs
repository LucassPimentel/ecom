using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace products_api.Entities
{
    [Table("Products")]
    public record Product(
        int Id,
        int CategoryId,
        string Name,
        string? Description,
        decimal Price,
        int Stock,
        string? ImageUrl,
        Category Category,
        DateTime CreatedAt = default,
        DateTime? UpdatedAt = default
    );
}
