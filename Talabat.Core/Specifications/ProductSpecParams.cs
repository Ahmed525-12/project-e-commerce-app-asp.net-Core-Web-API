using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
	public class ProductSpecParams
	{
        public string? sort { get; set; }
        public string? name { get; set; }
        public int? typeId { get; set; }
        public int? brandId { get; set; }

		private int pageSize = 5;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > 10 ? 10 : value; }
		}

		public int index { get; set; } = 1;


    }
}
