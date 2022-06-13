using ArvatoApp.Models;

namespace ArvatoApp.Data {
	public interface ITicketData {
		public SaveResult saveTicket(CustomerTicket ticket);
	}

	public class TicketData : ITicketData {

		private readonly EFContext _dbcontext;
		public TicketData(EFContext dbcontext) {
			_dbcontext = dbcontext;
		}
		public SaveResult saveTicket(CustomerTicket ticket) {
			// If the terms was not accepted, it will not create ticket
			SaveResult result = new SaveResult();
			if (!ticket.isTermsAccepted) {
				result.SaveResultMessage = "Please accept the terms.";
				return result;
			}
			_dbcontext.CustomerTickets.Add(ticket);
			_dbcontext.SaveChanges();
			result.extraInfo = ticket.ticketId;
			if (ticket.ticketId < 1) {
				result.SaveResultMessage = "Error in saving ticket, please try again later <br/>";
			}
			return result;
		}
	}
}
