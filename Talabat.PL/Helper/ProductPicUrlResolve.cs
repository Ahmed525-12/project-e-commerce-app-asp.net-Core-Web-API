using AutoMapper;
using Talabat.Core.Entities;
using Talabat.PL.DTOs;

namespace Talabat.PL.Helper
{
	public class ProductPicUrlResolve : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPicUrlResolve(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_configuration["APIBaseUrl"]}{source.PictureUrl}";
			}

			return string.Empty;
		}
	}
}
