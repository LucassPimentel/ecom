namespace products_api.DTOs
{
    public class ProductDTO
    {
        public ProductDTO(string name, string? description, decimal price, int stock, int categoryId, DateTime? createdAt, DateTime? updatedAt)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
