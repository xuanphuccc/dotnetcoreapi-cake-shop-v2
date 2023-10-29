using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi.cake.shop.application
{
    public class ShippingMethodResponseDto
    {
        public int ShippingMethodId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal InitialCharge { get; set; }

        [Required]
        public double InitialDistance { get; set; }

        [Required]
        public decimal AdditionalCharge { get; set; }

        [StringLength(450)]
        public string? Logo { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        public DateTime? CreateAt { get; set; }
    }
}
