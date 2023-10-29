using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetcoreapi_cake_shop.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

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

        [StringLength(200)]
        public string? DeliveryNotes { get; set; }

        [Required]
        public bool IsGift { get; set; }

        [StringLength(50)]
        public string? RecipientName { get; set; }

        [StringLength(20)]
        public string? RecipientPhoneNumber { get; set; }

        [Required]
        public decimal ShippingFee { get; set; }

        [Required]
        public decimal OrderTotal { get; set; }

        public DateTime CreateAt { get; set; }


        public int? ShippingMethodId { get; set; }
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod? ShippingMethod { get; set; }

        public int? OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus? OrderStatus { get; set; }

        public List<OrderItem>? Items { get; set;}
    }
}
