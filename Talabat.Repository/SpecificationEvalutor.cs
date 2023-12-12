using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Talabat.Core;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public static class SpecificationEvalutor<T> where T : BaseEntity
	{
		//_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

		public static async Task<IQueryable<T>> GetQuery(IQueryable<T> InputQuery,ISpecifications<T> Spec)
		{
			var Query = InputQuery;

			//.Where(P => P.Id == id)
			if (Spec.Criteria is not null)
				Query = Query.Where(Spec.Criteria);

			Spec.Count = await Query.CountAsync();

			if(Spec.OrderBy is not null) 
				Query = Query.OrderBy(Spec.OrderBy);

			if(Spec.OrderByDesc is not null) 
				Query = Query.OrderByDescending(Spec.OrderByDesc);

			if (Spec.IsPaginationEnable)
			{
				Query = Query.Skip(Spec.Skip).Take(Spec.Take);
			}

			// .Include(P => P.ProductType).Include(P => P.ProductBrand)

			Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));


			return Query;
		}
	}
}
