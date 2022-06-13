namespace ArvatoApp.Models {
	public class SaveResult {
		public bool Success { get { return string.IsNullOrWhiteSpace(SaveResultMessage); } }
		public string SaveResultMessage { get; set; }
		public int extraInfo { get; set; }

	}
}
