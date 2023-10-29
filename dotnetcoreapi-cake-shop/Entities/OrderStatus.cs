using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Entities
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;


        public List<Order>? Orders { get; set; }
    }
}
