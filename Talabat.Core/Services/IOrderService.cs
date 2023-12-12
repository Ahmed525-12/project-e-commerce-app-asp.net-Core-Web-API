using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Services
{
	public interface IOrderService
	{
		public Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, OrderAddress ShippingAddress);
		public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string BuyerEmail);
		public Task<Order> GetOrderByIdForUserAsync(string BuyerEmail, int OrderId);

		public Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();

	}
}
