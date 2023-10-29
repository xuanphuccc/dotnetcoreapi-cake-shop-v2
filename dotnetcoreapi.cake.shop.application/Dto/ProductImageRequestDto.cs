using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi.cake.shop.application
{
    public class ProductImageRequestDto
    {
        [Required]
        [StringLength(450)]
        public string Image { get; set; } = string.Empty;
    }
}
