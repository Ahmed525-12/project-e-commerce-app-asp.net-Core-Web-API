using System.Security.Cryptography;
using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.PL.DTOs;

namespace Talabat.PL.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(PR=>PR.Brand, O=>O.MapFrom(P=>P.Brand.Name))
                .ForMember(PR=>PR.Type, O=>O.MapFrom(P=>P.Type.Name))
                .ForMember(PR=>PR.PictureUrl, O=>O.MapFrom<ProductPicUrlResolve>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, OrderAddress>();

            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
			CreateMap<BasketItemDto, BasketItem>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                    .ForMember(OR => OR.DeliveryMethod, S => S.MapFrom(O => O.DeliveryMethod.ShortName))
                    .ForMember(OR => OR.DeliveryMethodCost, S => S.MapFrom(O => O.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, S => S.MapFrom(O => O.Product.ProductId))
                .ForMember(D => D.ProductName, S => S.MapFrom(O => O.Product.ProductName))
                .ForMember(D => D.PictureUrl, S => S.MapFrom(O=>O.Product.PictureUrl))
                .ForMember(D => D.PictureUrl, S => S.MapFrom<OrderPicResolver>());


		}
	}
}
