namespace Agape316.Data;

public partial class Event
{
    public Event()
    {
    }

    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Location { get; set; }
    public string? ContactEmail { get; set; }
    public DateTime Created { get; set; }
    public DateTime EventDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public int? SandwichSlots { get; set; }
    public int? SideDishSlots { get; set; }
    public int? MainDishSlots { get; set; }
    public int? DessertSlots { get; set; }
    public int? SetUpSlots { get; set; }
    public int? ServeSlots { get; set; }
    public int? CleanUpSlots { get; set; }
    public virtual IEnumerable<EventDish> EventDishes { get; set; }

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
                link = $"<a href='#' class='disabled-link'>Edit</a>";
            }
            else if (!string.IsNullOrEmpty(userName) && userName.Equals(ContactEmail))
            {
                link = $"<a href='#' class='enabled-link' onclick='EditEvent({Id})'>Edit</a>";
            }
            return link;
        }
    }
}
