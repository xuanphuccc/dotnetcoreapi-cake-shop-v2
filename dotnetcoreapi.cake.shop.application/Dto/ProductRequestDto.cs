using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi.cake.shop.application
{
    public class ProductRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [StringLength(256)]
        public string? Taste { get; set; }

        public string? Texture { get; set; }

        [StringLength(200)]
        public string? Size { get; set; }

        public string? Accessories { get; set; }

        public string? Instructions { get; set; }

        [Required]
        public bool IsDisplay { get; set; }

        public int? CategoryId { get; set; }

        [Required]
        public List<ProductImageRequestDto>? Images { get; set; }
    }
}
