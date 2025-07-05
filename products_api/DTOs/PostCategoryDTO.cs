using System.ComponentModel.DataAnnotations;

namespace products_api.DTOs
{
    public class PostCategoryDTO
    {
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
    }
}