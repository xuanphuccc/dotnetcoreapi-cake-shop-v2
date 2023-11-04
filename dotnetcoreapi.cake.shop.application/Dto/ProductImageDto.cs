using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi.cake.shop.application
{
    public class ProductImageDto
    {
        public int ProductImageId { get; set; }

        [Required]
        [StringLength(450)]
        public string Image { get; set; } = string.Empty;
    }
}
