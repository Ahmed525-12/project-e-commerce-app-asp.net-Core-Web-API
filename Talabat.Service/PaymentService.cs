using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
			_configuration = configuration;
			_basketRepository = basketRepository;
			_unitOfWork = unitOfWork;
		}
        public async Task<CustomerBasket?> CreateeOrUpdatePaymentIntent(string BasketId)
		{
			// Secret Key
			StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];

			// Get Basket
			var basket = await _basketRepository.GetBasketAsync(BasketId);
			if (basket == null) return null;

			decimal ShippingPrice = 0M;
			if (basket.DeliveryMethodId.HasValue)
			{
				var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
				ShippingPrice = DeliveryMethod.Cost;
			}

			// Ensure That Price Is Right
			if (basket.Items.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					if (product.Price != item.Price)
						item.Price = product.Price;
				}
			}

			var SubTotal = basket.Items.Sum(item => (item.Price * item.Quantity));

			// Create Payment Intent

			var Service = new PaymentIntentService();
			PaymentIntent paymentIntent;

			if (string.IsNullOrEmpty(basket.PaymentIntentId)) // Create New Payment Itent
			{
				var Option = new PaymentIntentCreateOptions()
				{
					Amount = (long) SubTotal *100 + (long)ShippingPrice * 100,
					Currency = "usd",
					PaymentMethodTypes = new List<string>() { "card" }
				};
				paymentIntent = await Service.CreateAsync(Option);
				basket.ClientSecret = paymentIntent.ClientSecret;
				basket.PaymentIntentId = paymentIntent.Id;
			}
			else // Update Current Payment Intent
			{
				var Options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)SubTotal * 100 + (long)ShippingPrice * 100,
				};
				paymentIntent = await Service.UpdateAsync(basket.PaymentIntentId, Options);
				basket.ClientSecret = paymentIntent.ClientSecret;
				basket.PaymentIntentId = paymentIntent.Id;

			}
			await _basketRepository.UpdateBasketAsync(basket);
			return basket;

		}
	}
}
