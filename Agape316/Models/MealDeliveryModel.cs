using Agape316.Data;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class MealDeliveryModel
    {
        private readonly IMealDelivery _mealDeliveryService;
        private readonly IMealSchedule _mealScheduleService;
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string email { get; private set; }
        public string phone { get; private set; }
        public string notes { get; private set; }
        public string deliveryTime { get; private set; }

        public MealDeliveryModel()
        {
        }

        public MealDeliveryModel(IMealSchedule mealScheduleService, IMealDelivery mealDeliveryService, int? scheduleId = null, int ? id = null)
        {
            _mealScheduleService = mealScheduleService;
            _mealDeliveryService = mealDeliveryService;            

            if (scheduleId.HasValue)
            {
                SelectedSchedule = _mealScheduleService.GetById(scheduleId.Value);
            }
            else
            {
                SelectedSchedule = new MealSchedule
                {
                    Created = DateTime.Now,
                };
            }

            if (id.HasValue)
            {
                var mealDelivery = _mealDeliveryService.GetById(id ?? 0);                
                if (mealDelivery != null)
                {
                    FirstName = mealDelivery.FirstName;
                    LastName = mealDelivery.LastName;
                    Email = mealDelivery.Email;
                    Phone = mealDelivery.Phone;
                    DeliveryDate = mealDelivery.DeliveryDate;
                    Notes = mealDelivery.Notes;
                }
                else
                {
                    mealDelivery = new MealDelivery
                    {
                        Created = DateTime.Now,
                        DeliveryDate = DateTime.Now
                    };
                }
            }
        }

        public MealSchedule SelectedSchedule { get; set; }

        public int? Id { get; set; }
        public int? ScheduleId { get; set; }
        public DateTime Created { get; set; }

        [Required]
        [Display(Name = "Delivery Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:  hh:mm:ss aa}")]
        public string DeliveryTime
        {
            get => deliveryTime;
            set => deliveryTime = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(100, ErrorMessage = "First name is required")]
        public string FirstName
        {
            get => firstName;
            set => firstName = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(100, ErrorMessage = "Last name is required")]
        public string LastName
        {
            get => lastName;
            set => lastName = new HtmlSanitizer().Sanitize(value);
        }
             
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get => email;
            set => email = new HtmlSanitizer().Sanitize(value == null ? "" : value);
        }
        
        [Display(Name = "Phone")]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string Phone
        {
            get => phone;
            set => phone = new HtmlSanitizer().Sanitize(value == null ? "" : value);
        }

        [Required]
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DeliveryDate { get; set; } = DateTime.Now.Date;

        public string DeliveryDateStr => DeliveryDate.ToShortDateString();

        public string? FullName => $"{FirstName} {LastName}";

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Notes
        {
            get => notes;
            set => notes = new HtmlSanitizer().Sanitize(value);
        }

        public async Task SaveMealDelivery(IEmailSender emailSender, MealDeliveryModel model, IMealDelivery _mealDeliveryService, IMealSchedule mealScheduleService)
        {
            if (!model.Id.HasValue)
            {
                var mealDelivery = new MealDelivery
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    DeliveryDate = model.DeliveryDate,
                    DeliveryTime = model.DeliveryTime,
                    ScheduleId = model.ScheduleId ?? 0,
                    Notes = model.Notes,
                    
                    Created = DateTime.Now,
                };
                await _mealDeliveryService.Create(mealDelivery);
            }
            else
            {
                var mealDelivery = _mealDeliveryService.GetById(model.Id.Value);
                mealDelivery.FirstName = model.FirstName;
                mealDelivery.LastName = model.LastName;
                mealDelivery.Email = model.Email;
                mealDelivery.Phone = model.Phone;
                mealDelivery.DeliveryDate = model.DeliveryDate;
                mealDelivery.DeliveryTime = model.DeliveryTime;
                mealDelivery.ScheduleId = model.ScheduleId ?? 0;
                mealDelivery.Notes = model.Notes;
                mealDelivery.Created = DateTime.Now;

                _mealDeliveryService.UpdateMealDelivery(mealDelivery);
            }
            MealSchedule mealSchedule = new MealSchedule();
            if (model.ScheduleId.HasValue)
            {
                mealSchedule = mealScheduleService.GetById(model.ScheduleId ?? 0);
            }
            await emailSender.SendEmailAsync(
                    model.Email,
                    "Delivery Information for " + mealSchedule.Title,
                    $"Description: {mealSchedule.Description}" +
                    $" <br /> Meal Schedule Coordinator: {mealSchedule.Coordinator} " +
                    $" <br /> Meal Schedule Start Date: {mealSchedule.StartDate.ToShortDateString()} " +
                    $" <br /> Meal Schedule End Date: {mealSchedule.EndDate.ToShortDateString()} " +
                    $" <br /> Meal Schedule Recipient: {mealSchedule.RecipientFullName}" +
                    $" <br /> Delivered By: {model.FullName}" +
                    $" <br /> Delivery Date/Time: {model.DeliveryDate.ToShortDateString()} - {Agape316.Helpers.EventHelpers.GetStandardTimeFromMilitaryTime(model.DeliveryTime)}" +
                    $" <br /> Contact Email: {model.Email}" +
                    $" <br /> Contact Phone: {model.Phone}" +
                    $" <br /> Notes: {model.Notes}"
            );
        }
    }
}
