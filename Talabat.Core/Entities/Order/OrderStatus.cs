using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
	public enum OrderStatus
	{
		[EnumMember(Value = "Pending")]
		Pending,

		[EnumMember(Value = "Received")]
		Received,

		[EnumMember(Value = "Failed")]
		Failed

	}
}
