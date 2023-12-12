using System.ComponentModel.DataAnnotations;

namespace Talabat.PL.DTOs
{
	public class RegisterDto
	{

		[Required]
		[EmailAddress]
        public string Email { get; set; }

		[Required]
        public string DisplayName { get; set; }

		[Phone]
		[Required]
		public string PhoneNumber { get; set; }

		[Required]
        public string Password { get; set; }
    }
}
