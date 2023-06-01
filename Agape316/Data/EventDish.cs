using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class EventDish
{
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

    public string ContactLink
    {
        get
        {
            string link = string.Empty;
            var httpContext = new HttpContextAccessor().HttpContext;
            var userName = httpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                link = $"<a href='#' class='disabled-link'>Contact</a>";
            }
            else if (!string.IsNullOrEmpty(userName) && userName.Equals(ContactEmail))
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
            var httpContext = new HttpContextAccessor().HttpContext;
            var userName = httpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                link = $"<a href='#' class='disabled-link')'>Edit</a>";
            }
            else if (!string.IsNullOrEmpty(userName) && userName.Equals(ContactEmail))
            {
                link = $"<a href='#' class='enabled-link' onclick='EditEventDish({EventId},{Id})'>Edit</a>";
            }
            return link;
        }
    }
}
