using ArvatoApp.Data;
using ArvatoApp.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ArvatoApp.Pages {
	public class IndexModel : PageModel {
		[BindProperty]
		public CustomerTicket Ticket { get; set; }
		private readonly EFContext _dbcontext;
		private readonly ITicketData ticketData;


		public IndexModel(EFContext dbcontext) {
			_dbcontext = dbcontext;
			ticketData = new TicketData(dbcontext);
		}

		public void OnGet() {

		}
		public IActionResult OnPost() {
			//data is validating
			if (!ModelState.IsValid) {
				//return UnprocessableEntityObjectResult(ModelState);
				var errors = ModelState.Select(x => x.Value.Errors)
					.Where(y => y.Count > 0).Select(x => x[0].ErrorMessage)
					.ToList();
				ViewData["info"] = "Please validate your data, " + string.Join(",", errors) + "<br/>";
				return Page();
			}

			// to keep extra records log IP/browser data/Date will collect
			Ticket.IP = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
			Ticket.browserInfo = Request.Headers["User-Agent"].ToString();
			Ticket.logDateTime = DateTime.Now;


			var result = ticketData.saveTicket(Ticket);
			if (!result.Success) {
				ViewData["info"] = result.SaveResultMessage;
				return Page();
			}

			ViewData["info"] = "Ticket created with ID: " + result.extraInfo + "<br/>";
			Ticket = new CustomerTicket();
			ModelState.Clear();
			return Page();
		}
	
	}

}