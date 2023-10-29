using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi.cake.shop.application
{
    public class OrderRequestDto
    {
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string CustomerPhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [StringLength(100)]
        public string DeliveryTime { get; set; } = string.Empty;

        [Required]
        [StringLength(450)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public double Distance { get; set; }

        [StringLength(200)]
        public string? DeliveryNotes { get; set; }

        [Required]
        public bool IsGift { get; set; }

        [StringLength(50)]
        public string? RecipientName { get; set; }

        [StringLength(20)]
        public string? RecipientPhoneNumber { get; set; }

        [Required]
        public List<OrderItemRequestDto>? Items { get; set; }
    }
}
