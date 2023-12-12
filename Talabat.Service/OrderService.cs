using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Order;

namespace Talabat.Service
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IBasketRepository basketRepository, 
			IUnitOfWork UnitOfWork)
        {
			_basketRepository = basketRepository;
			_unitOfWork = UnitOfWork;
		}
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, OrderAddress ShippingAddress)
		{
			//1.Get Basket From Basket Repo

			var Basket = await _basketRepository.GetBasketAsync(BasketId);

			//2.Get Selected Items at Basket From Product Repo

			var OrderItems = new List<OrderItem>();

			if(Basket?.Items.Count > 0)
			{
				foreach (var item in Basket.Items)
				{
					var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					var ProductItemOrder = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
					var orderItem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);
					OrderItems.Add(orderItem);
				}
			}

			//3.Calculate SubTotal

			var subTotal = OrderItems.Sum(O => O.Quantity * O.Price);

			//4.Get Delivery Method From DeliveryMethod Repo
			var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

			//5.Create Order
			var order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, subTotal);

			//6.Add Order Locally
			await _unitOfWork.Repository<Order>().AddAsync(order);

			//7.Save Order To Database 
			var result = await _unitOfWork.CompleteAsync();
			
			return result > 0 ? order : null;

		}

		public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
		{
			var DeliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
			return (IReadOnlyList<DeliveryMethod>) DeliveryMethods;
		}

		public async Task<Order> GetOrderByIdForUserAsync(string BuyerEmail, int OrderId)
		{
			var Spec = new OrderSpecifications(BuyerEmail, OrderId);
			var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(Spec);

			return order;
		}

		public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string BuyerEmail)
		{
			var Spec = new OrderSpecifications(BuyerEmail);
			var Orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);

			return Orders; 
		}
	}
}
