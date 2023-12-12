using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.Order
{
	public class OrderSpecifications :Specifications<Entities.Order_Aggregate.Order>
	{
		public OrderSpecifications(string buyerEmail) : base(O=>O.BuyerEmail == buyerEmail)
		{
			Includes.Add(O => O.DeliveryMethod);
			Includes.Add(O => O.Items);

			AddOrderByDesc(O => O.OrderDate);
			IsPaginationEnable = false;

		}

		public OrderSpecifications(string buyerEmail, int OrderId) : base(O => O.BuyerEmail == buyerEmail && O.Id == OrderId)
		{
			Includes.Add(O => O.DeliveryMethod);
			Includes.Add(O => O.Items);

			IsPaginationEnable = false;

		}
	}
}
