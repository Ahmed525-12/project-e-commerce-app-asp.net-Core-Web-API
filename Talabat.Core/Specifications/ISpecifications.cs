using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity
	{
		//_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

		// Sign For Property For Where [Where(P => P.Id == id)]
		public Expression<Func<T,bool>> Criteria { get; set; }

        // Sign For Property For Include [Include(P => P.ProductType).Include(P => P.ProductBrand)]
        public List<Expression<Func<T,object>>> Includes { get; set; }

		// Property for Order by Asc
        public Expression<Func<T, object>> OrderBy { get; set; }

		// Property For Order By Desc
        public Expression<Func<T, object>> OrderByDesc { get; set; }

        // Property For Skip() and Take() For Pagination
        public int Skip { get; set; }
        public int Take { get; set; }
		public bool IsPaginationEnable { get; set; }


        public int Count { get; set; }

    }
}
