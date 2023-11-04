using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi.cake.shop.application
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Wishes { get; set; }

        public ProductDto? Product { get; set; }
    }
}
