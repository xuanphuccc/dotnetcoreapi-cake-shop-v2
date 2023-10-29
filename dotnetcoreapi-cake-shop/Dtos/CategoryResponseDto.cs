using dotnetcoreapi_cake_shop.Entities;
using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Dtos
{
    public class CategoryResponseDto
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [StringLength(450)]
        public string Image { get; set; } = string.Empty;

        public DateTime? CreateAt { get; set; }
    }
}
