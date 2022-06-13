using ArvatoApp.Data;
using ArvatoApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace ArvatoAppTest {
	public class AppTest {
		private readonly EFContext _dbcontext;
		private readonly ITicketData ticketData;
		public AppTest() {
			var options = new DbContextOptionsBuilder<EFContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
			_dbcontext = new EFContext(options);
			ticketData = new TicketData(_dbcontext);

		}
		[Theory]
		[InlineData("volkanbal@hotmail.com", "string", null, "", true, InquiryTypes.type_a, false)]
		[InlineData("volkan.bal@hotmail.com", "5355581300", 1234, "", true, InquiryTypes.type_a, false)]
		[InlineData("volkanbalm", "5355581300", null, "Something", false, InquiryTypes.type_a, false)]
		[InlineData("volkanbalm", "5355581300", null, "Something", true, InquiryTypes.type_a, false)]
		[InlineData("volkanbalm", "5355581300", 123, "Something", true, InquiryTypes.type_a, false)]
		[InlineData("volkanbal@hotmail.com", "5355581300", 123, "Something", true, InquiryTypes.type_a, true)]
		[InlineData("volkanbal@hotmail.com", "5355581300", 123, "Something", false, InquiryTypes.type_a, false)]

		public async Task SaveTicket(string customerEmail, string customerPhone, long? customerNumber, string inquiryDescription, bool isTermsAccepted, InquiryTypes typeOfInquiry, bool pass) {
			var Ticket = new CustomerTicket() {
				customerEmail = customerEmail,
				customerPhone = customerPhone,
				customerNumber = customerNumber,
				inquiryDescription = inquiryDescription,
				isTermsAccepted = isTermsAccepted,
				IP = "127.0.0.1",
				browserInfo = "Test Agent",
				logDateTime = DateTime.Now,
			};
			var modelValidationResult = ValidateModel(Ticket).Count == 0;
			if (pass && !modelValidationResult)
				Assert.Equal(pass, modelValidationResult);
			var result = ticketData.saveTicket(Ticket);
			Assert.Equal(pass, result.Success && modelValidationResult);
		}

		// I kept that function result as list not bool because there might be extra test function with testing individual properties
		private IList<ValidationResult> ValidateModel(object model) {
			var validationResults = new List<ValidationResult>();
			var ctx = new ValidationContext(model, null, null);
			Validator.TryValidateObject(model, ctx, validationResults, true);
			return validationResults;
		}
	}

}