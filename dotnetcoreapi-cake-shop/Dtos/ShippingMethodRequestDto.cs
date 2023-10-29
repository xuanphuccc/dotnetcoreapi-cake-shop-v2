using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Dtos
{
    public class ShippingMethodRequestDto
    {
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
    }
}
