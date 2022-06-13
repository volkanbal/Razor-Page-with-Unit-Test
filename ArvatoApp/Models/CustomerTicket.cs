using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArvatoApp.Models {
	public class CustomerTicket {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ticketId { get; set; }
		[Required(ErrorMessage = "e-Mail is required")]
		//[EmailAddress(ErrorMessage = "Please enter a valid email")]
		[RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Please enter a valid email")]
		[StringLength(100)]
		public string customerEmail { get; set; } = "";

		[Required(ErrorMessage = "Phone Number is required")]
		[RegularExpression(@"^\+?[1-9][0-9]{7,14}$", ErrorMessage = "Please enter a valid phone number like +905355581300")]
		[StringLength(20)]
		public string customerPhone { get; set; } = "";

		[RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid customer number just with digits.")]
		public long? customerNumber { get; set; }

		[Required(ErrorMessage = "Please describe the problem briefly.")]
		[MinLength(5)]
		public string inquiryDescription { get; set; } = "";

		[Required(ErrorMessage = "Please read terms and select agreeing terms.")]
		public bool isTermsAccepted { get; set; } = false;
		[Required(ErrorMessage = "Please specify the type of your inquiry.")]
		public InquiryTypes typeOfInquiry { get; set; } = InquiryTypes.type_a;

		public DateTime logDateTime { get; set; } = DateTime.Now;
		public string? IP { get; set; }
		public string? browserInfo { get; set; }
	}

}
