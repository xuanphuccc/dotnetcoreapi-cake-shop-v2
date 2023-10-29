using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi.cake.shop.application
{
    public class OrderItemRequestDto
    {
        [Required]
        public int Qty { get; set; }

        [StringLength(200)]
        public string? Wishes { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
