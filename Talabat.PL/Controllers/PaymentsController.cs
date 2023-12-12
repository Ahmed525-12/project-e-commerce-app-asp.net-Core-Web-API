using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Services;
using Talabat.PL.DTOs;
using Talabat.PL.Errors;

namespace Talabat.PL.Controllers
{
	[Authorize]
	public class PaymentsController : APIBaseController
	{
		private readonly IPaymentService _paymentService;
		private readonly IMapper _mapper;

		public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
			_paymentService = paymentService;
			_mapper = mapper;
		}


        [HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
		{
			var customerBasket = await _paymentService.CreateeOrUpdatePaymentIntent(BasketId);
			if (customerBasket == null) return BadRequest(new ApiResponse(404, "There Is a Problem With Your Chart"));
			var MappedBasket = _mapper.Map<CustomerBasket, CustomerBasketDto>(customerBasket);
			return Ok(MappedBasket);
		}
	}
}
