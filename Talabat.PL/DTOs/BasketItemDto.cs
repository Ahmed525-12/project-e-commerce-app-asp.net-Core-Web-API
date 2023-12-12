using System.ComponentModel.DataAnnotations;

namespace Talabat.PL.DTOs
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string ProductName { get; set; }

		[Required]
		public string PictureUrl { get; set; }

		[Required]
		public string Brand { get; set; }

		[Required]
		public string Type { get; set; }

		[Required]
		[Range(0.1,double.MaxValue, ErrorMessage ="The Price Can't be 0")]
		public decimal Price { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "The Quantity Can't be 0")]
		public int Quantity { get; set; }
	}
}