using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.PL.DTOs
{
	public class OrderToReturnDto
	{
		public int Id { get; set; }
		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public string Status { get; set; } 
		public OrderAddress ShippingAddress { get; set; }
		public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
		public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }
	}
}
