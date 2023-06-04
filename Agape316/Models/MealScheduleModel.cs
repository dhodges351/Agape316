using Agape316.Data;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class MealScheduleModel
    {
		private readonly IMealSchedule _mealScheduleService;
        public string title { get; private set; }
        public string description { get; private set; }
        public string coordinator { get; private set; }
		public string coordEmail { get; private set; }
		public string coordPhone { get; private set; }
		public string recipientFName { get; private set; }
		public string recipientLName { get; private set; }
        public string recipientEmail { get; private set; }
        public string recipientPhone { get; private set; }
		public string recipientAddress { get; private set; }
        public string recipientCity { get; private set; }
        public string recipientState { get; private set; }
        public string recipientZipcode { get; private set; }
        public string foodAllergies { get; private set; }
        public string notes { get; private set; }

        public MealScheduleModel()
        {
        }

        public MealScheduleModel(IMealSchedule mealScheduleService, int? id = null)
        {
            _mealScheduleService = mealScheduleService;
            if (id.HasValue)
			{
				var mealSchedule = _mealScheduleService.GetById(id ?? 0);
				if (mealSchedule != null)
				{
                    Title = mealSchedule.Title;
                    Description = mealSchedule.Description;
					Coordinator = mealSchedule.Coordinator;
                    CoordEmail = mealSchedule.CoordEmail;
                    CoordPhone = mealSchedule.CoordPhone;
                    RecipientFName = mealSchedule.RecipientFName;
                    RecipientLName = mealSchedule.RecipientLName;
                    RecipientEmail = mealSchedule.RecipientEmail;
                    RecipientPhone = mealSchedule.RecipientPhone;
                    RecipientAddress = mealSchedule.RecipientAddress;
                    RecipientCity = mealSchedule.RecipientCity;
                    RecipientState = mealSchedule.RecipientState;
                    RecipientZipcode = mealSchedule.RecipientZipcode;
                    StartDate = mealSchedule.StartDate;
                    EndDate = mealSchedule.EndDate;
                    Monday = mealSchedule.Monday;
                    Tuesday = mealSchedule.Tuesday;
                    Wednesday = mealSchedule.Wednesday;
                    Thursday = mealSchedule.Thursday;
                    Friday = mealSchedule.Friday;
                    Saturday = mealSchedule.Saturday;
                    Sunday = mealSchedule.Sunday;
                    FoodAllergies = mealSchedule.FoodAllergies;
                    Notes = mealSchedule.Notes;
                }
			}
		}

        public int? Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title is required")]
        public string Title
        {
            get => title;
            set => title = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(200, ErrorMessage = "Description is required")]
        public string Description
        {
            get => description;
            set => description = new HtmlSanitizer().Sanitize(value);
        }      

        [Required]
        [StringLength(100, ErrorMessage = "Coordinator is required")]
        public string Coordinator
        {
            get => coordinator;
            set => coordinator = new HtmlSanitizer().Sanitize(value);
        }       

        [Required]
        [Display(Name = "Coordinator Email")]
        [StringLength(100, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string CoordEmail
        {
            get => coordEmail;
            set => coordEmail = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Coordinator Phone")]
        [StringLength(100, ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string CoordPhone
        {
            get => coordPhone;
            set => coordPhone = new HtmlSanitizer().Sanitize(value);
        }      

        [Required]
        [Display(Name = "Recipient First Name")]
        [StringLength(100, ErrorMessage = "Recipient First Name is required")]
        public string RecipientFName
        {
            get => recipientFName;
            set => recipientFName = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Recipient Last Name")]
        [StringLength(100, ErrorMessage = "Recipient Last Name is required")]
        public string RecipientLName
        {
            get => recipientLName;
            set => recipientLName = new HtmlSanitizer().Sanitize(value);
        }
        
        [Display(Name = "Recipient Email")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string RecipientEmail
        {
            get => recipientEmail;
            set => recipientEmail = new HtmlSanitizer().Sanitize(value);
        }
        
        [Display(Name = "Recipient Phone")]
        [StringLength(100)]
        [DataType(DataType.PhoneNumber)]
        public string RecipientPhone
        {
            get => recipientPhone;
            set => recipientPhone = new HtmlSanitizer().Sanitize(value);
        } 

        [Required]
        [Display(Name = "Recipient Address")]
        [StringLength(500, ErrorMessage = "Recipient address is required")]
        public string RecipientAddress
        {
            get => recipientAddress;
            set => recipientLName = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Recipient City")]
        [StringLength(100, ErrorMessage = "Recipient city is required")]
        public string RecipientCity
        {
            get => recipientCity;
            set => recipientCity = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Recipient State")]
        [StringLength(2, ErrorMessage = "Recipient state is required")]
        public string RecipientState
        {
            get => recipientState;
            set => recipientState = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Recipient Zipcode")]
        [StringLength(10, ErrorMessage = "Recipient zipcode is required")]
        public string RecipientZipcode
        {
            get => recipientZipcode;
            set => recipientZipcode = new HtmlSanitizer().Sanitize(value);
        }

        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        [Display(Name = "Food Allergies")]
        [StringLength(500)]
        public string FoodAllergies
        {
            get => foodAllergies;
            set => foodAllergies = new HtmlSanitizer().Sanitize(value);
        }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Notes
        {
            get => notes;
            set => notes = new HtmlSanitizer().Sanitize(value);
        }


        public async Task SaveMealSchedule(IEmailSender emailSender, MealScheduleModel model, IMealSchedule _mealScheduleService)
        {
            if (!model.Id.HasValue)
            {
                var mealSchedule = new MealSchedule
                {
                    Title = model.Title,
                    Description = model.Description,
                    Coordinator = model.Coordinator,
                    CoordEmail = model.CoordEmail,
                    CoordPhone = model.CoordPhone,
                    RecipientFName = model.RecipientFName,
                    RecipientLName = model.RecipientLName,
                    RecipientEmail = model.RecipientEmail,
                    RecipientPhone = model.RecipientPhone,
                    RecipientAddress = model.RecipientAddress,
                    RecipientCity = model.RecipientCity,
                    RecipientState = model.RecipientState,
                    RecipientZipcode = model.RecipientZipcode,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Monday = model.Monday,
                    Tuesday = model.Tuesday,
                    Wednesday = model.Wednesday,
                    Thursday = model.Thursday,
                    Friday = model.Friday,
                    Saturday = model.Saturday,
                    Sunday = model.Sunday,
                    FoodAllergies = model.FoodAllergies,
                    Notes = model.Notes,
                };
                await _mealScheduleService.Create(mealSchedule);
            }
            else
            {
                var mealSchedule = _mealScheduleService.GetById(model.Id.Value);
                Title = mealSchedule.Title;
                Description = mealSchedule.Description;
                Coordinator = mealSchedule.Coordinator;
                CoordEmail = mealSchedule.CoordEmail;
                CoordPhone = mealSchedule.CoordPhone;
                RecipientFName = mealSchedule.RecipientFName;
                RecipientLName = mealSchedule.RecipientLName;
                RecipientEmail = mealSchedule.RecipientEmail;
                RecipientPhone = mealSchedule.RecipientPhone;
                RecipientAddress = mealSchedule.RecipientAddress;
                RecipientCity = mealSchedule.RecipientCity;
                RecipientState = mealSchedule.RecipientState;
                RecipientZipcode = mealSchedule.RecipientZipcode;
                StartDate = mealSchedule.StartDate;
                EndDate = mealSchedule.EndDate;
                Monday = mealSchedule.Monday;
                Tuesday = mealSchedule.Tuesday;
                Wednesday = mealSchedule.Wednesday;
                Thursday = mealSchedule.Thursday;
                Friday = mealSchedule.Friday;
                Saturday = mealSchedule.Saturday;
                Sunday = mealSchedule.Sunday;
                FoodAllergies = mealSchedule.FoodAllergies;
                Notes = mealSchedule.Notes;

                _mealScheduleService.UpdateMealSchedule(mealSchedule);
            }
            await emailSender.SendEmailAsync(
                    model.CoordEmail,
                    model.Title,
                    $" <br /> Coordinator: {model.Coordinator} " +
                    $" <br /> Description: {model.Description} " +
                    $" <br /> Start Date: {model.StartDate.ToShortDateString()} " +
                    $" <br /> End Date: {model.EndDate.ToShortDateString()} " +
                    $" <br /> Recipient: {model.RecipientFName} {model.recipientLName}" +
                    $" <br /> Phone: {model.RecipientPhone}" +
                    $" <br /> Email: {model.RecipientEmail}" +
                    $" <br /> Address: {model.recipientAddress} {model.recipientCity} {model.recipientState} {model.RecipientZipcode}" +
                    $" <br /> Days/Evenings: {(model.Monday ? "Monday" : "")} {(model.Tuesday ? "Tuesday" : "")}" +
                    $" {(model.Wednesday ? "Wednesday" : "")} {(model.Thursday ? "Thursday" : "")} {(model.Friday ? "Friday" : "")}" +
                    $" {(model.Saturday ? "Saturday" : "")} {(model.Sunday ? "Sunday" : "")} " +               
                    $" <br /> Food Allergies: {model.FoodAllergies}" +
                    $" <br /> Notes: {model.Notes}"
            );
        }
    }
}
