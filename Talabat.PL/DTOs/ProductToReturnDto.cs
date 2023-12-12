using Talabat.Core.Entities;

namespace Talabat.PL.DTOs
{
	public class ProductToReturnDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }
		public int BrandId { get; set; }
		public string Brand { get; set; }
		public int TypeId { get; set; }
		public string Type { get; set; }
	}
}
