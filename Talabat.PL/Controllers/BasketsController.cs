using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.PL.DTOs;
using Talabat.PL.Errors;

namespace Talabat.PL.Controllers
{
	public class BasketsController : APIBaseController
	{
		private readonly IBasketRepository _basket;
		private readonly IMapper _mapper;

		public BasketsController(IBasketRepository basket, IMapper mapper)
        {
			_basket = basket;
			_mapper = mapper;
		}


		// Get or Recreate Basket
		[HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket (string id)
		{
			var UserBasket = await _basket.GetBasketAsync(id);
			return UserBasket is null? new CustomerBasket(id) : Ok(UserBasket);
		}


		// Update Or Create Basket
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket (CustomerBasketDto customerBasket)
		{
			var MappedCustomerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);
			var UpdateOrCreateBasket = await _basket.UpdateBasketAsync(MappedCustomerBasket);
			return UpdateOrCreateBasket is null?  BadRequest(new ApiResponse(400)) : Ok(UpdateOrCreateBasket);
		}


		// Delete Basket
		[HttpDelete]
		public async Task<bool> DeleteBasket (string id)
		{
			return await _basket.DeleteBasketAsync(id);
		}
	}
}
