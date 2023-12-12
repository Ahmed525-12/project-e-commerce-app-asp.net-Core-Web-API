using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
	public class Specifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; } // where
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; } = true;


		public int Count { get; set; }


		//Get All
		public Specifications()
        {
            
        }

        // Get By Id
        public Specifications(Expression<Func<T, bool>>criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        //Order by Asc
        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        // Order By Desc
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }

        // Apply Pagination
        public void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
