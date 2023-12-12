using System.ComponentModel.DataAnnotations;

namespace Talabat.PL.DTOs
{
	public class OrderDto
	{
        [Required]
        public string BasketId { get; set; }
        [Required]
		public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
