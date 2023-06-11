namespace Agape316.Models
{
    public class AdminViewModel
    {
		public string? _notes { get; private set; }
		public string? _title { get; private set; }
		public string? _description { get; private set; }
		public string? _author { get; private set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Author { get; set; }
		public string? FileType { get; set; }
		public string? Notes { get; set; }
		public DateTime? CreatedDate { get; set; } = DateTime.Now;
	}
}