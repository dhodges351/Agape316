using Agape316.Data;
using Ganss.Xss;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class AdminViewModel
    {
        private readonly IAgapeDocument _agapeDocumentService;

        public AdminViewModel()
        {
        }

        public AdminViewModel(IAgapeDocument agapeDocumentService)
        {
            _agapeDocumentService = agapeDocumentService;
            AgapeDocuments = _agapeDocumentService.GetAll().ToList();
        }

        public string? _notes { get; private set; }
        public string? _title { get; private set; }
        public string? _description { get; private set; }
        public string? _author { get; private set; }
        public string? _nameOfFile { get; private set; }    
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public int? Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title is required")]
        public string? Title
        {
            get => _title;
            set => _title = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Filename")]
        [StringLength(100, ErrorMessage = "File name is required")]
        public string? NameOfFile
        {
            get => _nameOfFile;
            set => _nameOfFile = new HtmlSanitizer().Sanitize(value);
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

        public List<AgapeDocument> AgapeDocuments { get; set; }


        public async Task SaveDocument(AdminViewModel model, IAgapeDocument agapeDocumentService)
        {
            if (!model.Id.HasValue)
            {
                var agapeDocument = new AgapeDocument
                {
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    Author = model.Author,
                    FileType = model.FileType,
                    NameOfFile = model.NameOfFile,
                    Notes = model.Notes                   
                };
                await agapeDocumentService.Create(agapeDocument);
            }
            else
            {
                var agapeDocument = agapeDocumentService.GetById(model.Id.Value);
                agapeDocument.Title = model.Title;
                agapeDocument.Description = model.Description;
                agapeDocument.Notes = model.Notes;
                agapeDocument.FileType = model.FileType;
                agapeDocument.NameOfFile = model.NameOfFile;

                agapeDocumentService.UpdateAgapeDocument(agapeDocument);
            }
        }
    }
}