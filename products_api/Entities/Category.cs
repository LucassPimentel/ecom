using System.ComponentModel.DataAnnotations.Schema;

namespace products_api.Entities
{
    [Table("Categories")]
    public record Category(
        int Id,
        string Title,
        List<Product> Products,
        DateTime CreatedAt = default,
        DateTime UpdatedAt = default
    );
}
