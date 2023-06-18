using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class EventDish
{
    public EventDish()
    {}   
    public int Id { get; set; }
    public int EventId { get; set; }
    public int? SandwichSlot { get; set; }
    public int? SideDishSlot { get; set; }
    public int? MainDishSlot { get; set; }
    public int? DessertSlot { get; set; }
    public int? SetUpSlot { get; set; }
    public int? CleanUpSlot { get; set; }
    public int? ServeSlot { get; set; }
    public DateTime? Created { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ContactEmail { get; set; }
    public string? ImageUrl { get; set; }
    public string? Notes { get; set; }
    public string? Category { get; set; }
    public Event? RelatedEvent { get; set; }
    public string UserName
    {
        get
        {
            string userName = string.Empty;
            if (Agape316.Helpers.AppContext.Current != null)
            {
                userName = Agape316.Helpers.AppContext.Current.Session.GetString("UserName");
            }
            return userName;
        }
    }
    public string ContactLink
    {
        get
        {
            string link = string.Empty;            
            if (string.IsNullOrEmpty(UserName))
            {
                link = $"<a href='#' class='disabled-link'>Contact</a>";
            }
            else if (!string.IsNullOrEmpty(UserName) && UserName.Equals(ContactEmail))
            {
                link = $"<a href='/Identity/Account/Profile/' class='enabled-link'>Contact</a>";
            }
            return link;
        }
    }
    public string EditLink
    {
        get
        {
            string link = string.Empty;
            Event agapeEvent = null;
            DateTime date1 = DateTime.Now;
            DateTime? date2 = null;
            bool isExpired = true;

            string connectionString = Agape316.Helpers.ConfigHelper.AppSetting("DefaultConnection");
            var contextOptions = new DbContextOptionsBuilder<Agape316.Data.ApplicationDbContext>()
            .UseSqlServer(connectionString)
            .Options;

            using (var context = new Agape316.Data.ApplicationDbContext(contextOptions))
            {
                agapeEvent = context.Event.Where(x => x.Id == EventId).FirstOrDefault();
                if (agapeEvent != null)
                {
                    date2 = DateTime.Parse($"{agapeEvent.EventDate.ToShortDateString()} {agapeEvent.EndTime}");
                    int result = DateTime.Compare(date1, date2.Value);
                    if (result <= 0)
                    {
                        isExpired = false;
                    }
                }
            };

            if (string.IsNullOrEmpty(UserName) || isExpired)
            {
                link = $"<a href='#' class='disabled-link')'>Edit</a>";
            }
            else if (!string.IsNullOrEmpty(UserName) && UserName.Equals(ContactEmail))
            {
                link = $"<a href='#' class='enabled-link' onclick='EditEventDish({EventId},{Id})'>Edit</a>";
            }
            return link;
        }
    }

    public string ImageUrlDisplay
    {
        get
        {
            return string.Empty;
        }
    }
}
