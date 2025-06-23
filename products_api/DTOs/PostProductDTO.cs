using System.ComponentModel.DataAnnotations;

namespace products_api.DTOs
{
    public class PostProductDTO
    {
        public PostProductDTO(string name, string? description, decimal price, int stock, int categoryId, DateTime? createdAt)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            CreatedAt = createdAt;
        }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        [MinLength(1)]
        public int Stock { get; set; }

        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
