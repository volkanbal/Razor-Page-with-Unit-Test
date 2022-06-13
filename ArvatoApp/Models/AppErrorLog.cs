using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArvatoApp.Models {
	public class AppErrorLog {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long errorID { get; set; }
		public string error { get; set; }
		public string errorCode { get; set; }
		public DateTime errorDate { get; set; }
		public AppErrorLog() { }
	}
}
