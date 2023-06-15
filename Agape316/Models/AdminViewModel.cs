using Ganss.Xss;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class AdminViewModel
    {
		public string? _notes { get; private set; }
		public string? _title { get; private set; }
		public string? _description { get; private set; }
		public string? _author { get; private set; }	
		public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100, ErrorMessage = "Title is required")]
        public string? Title
        {
            get => _title;
            set => _title = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(200, ErrorMessage = "Description is required")]
        public string? Description
        {
            get => _description;
            set => _description = new HtmlSanitizer().Sanitize(value);
        }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string? Notes
        {
            get => _notes;
            set => _notes = new HtmlSanitizer().Sanitize(value == null ? "" : value);
        }

        [Required]
        [StringLength(100, ErrorMessage = "Author is required")]
        public string? Author
        {
            get => _author;
            set => _author = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(100, ErrorMessage = "File Type is required")]
        public string? FileType { get; set; }
    }
}