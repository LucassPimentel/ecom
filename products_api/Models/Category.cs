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
    //{
    //    public Category(int categoryId, string name)
    //    {
    //        CategoryId = categoryId;
    //        Name = name;
    //    }

    //    public int CategoryId { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    //    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    //}
}
