using System.ComponentModel.DataAnnotations;

namespace products_api.DTOs
{
    public class PostProductDTO
    {
        public PostProductDTO(string name, string? description, decimal price, int stock, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;        }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; } = string.Empty;

        [MinLength(10)]
        public decimal Price { get; set; }

        [MinLength(1)]
        public int Stock { get; set; }

        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
