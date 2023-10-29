using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Dtos
{
    public class OrderItemResponseDto
    {
        public int OrderItemId { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Wishes { get; set; }

        public ProductResponseDto? Product { get; set; }
    }
}
