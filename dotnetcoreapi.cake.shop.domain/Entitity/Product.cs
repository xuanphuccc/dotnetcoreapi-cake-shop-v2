using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetcoreapi.cake.shop.domain
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "text")]
        public string? Description { get; set; }

        [StringLength(256)]
        public string? Taste { get; set; }

        [Column(TypeName = "text")]
        public string? Texture { get; set; }

        [StringLength(200)]
        public string? Size { get; set; }

        [Column(TypeName = "text")]
        public string? Accessories { get; set; }

        [Column(TypeName = "text")]
        public string? Instructions { get; set; }

        [Required]
        public bool IsDisplay { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public List<ProductImage>? Images { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
