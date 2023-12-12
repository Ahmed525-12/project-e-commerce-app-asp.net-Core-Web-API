using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	public class Product : BaseEntity
	{
        public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; }

        public int TypeId { get; set; }
        public ProductType Type { get; set; }
    }
}
