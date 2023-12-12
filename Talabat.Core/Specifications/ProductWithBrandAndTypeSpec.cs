using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndTypeSpec : Specifications<Product>
	{
		// Get All Product
        public ProductWithBrandAndTypeSpec(ProductSpecParams Params) :
			base(P=>
			(!Params.typeId.HasValue || P.TypeId == Params.typeId)
			&&
			(!Params.brandId.HasValue || P.BrandId == Params.brandId)
			&&
			(string.IsNullOrEmpty(Params.name) || P.Name.ToLower().Contains(Params.name.ToLower()))
			)

		{

			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Type);

			if (!string.IsNullOrEmpty(Params.sort))
			{
				switch (Params.sort)
				{
					case "PriceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "PriceDesc":
						AddOrderByDesc(P => P.Price);
						break;
					default:
						AddOrderBy(P => P.Name);
						break;
				}
			}

			// Page size = 10
			// Page index = 5
			// Skip => size * (index-1)
			// Take => size
			ApplyPagination(Params.PageSize * (Params.index - 1), Params.PageSize);
		}


		// Get Product By ID
		public ProductWithBrandAndTypeSpec(int id) : base(P => P.Id == id)
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Type);
		}
	}
}
