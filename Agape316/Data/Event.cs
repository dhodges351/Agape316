using System.Security.Claims;

namespace Agape316.Data;

public partial class Event
{        
    public Event()
    {}    
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Notes { get; set; }
    public string Location { get; set; }
    public string ContactEmail { get; set; }
    public DateTime Created { get; set; }
    public DateTime EventDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public int? SandwichSlots { get; set; }
    public int? SideDishSlots { get; set; }
    public int? MainDishSlots { get; set; }
    public int? DessertSlots { get; set; }
    public int? SetUpSlots { get; set; }
    public int? ServeSlots { get; set; }
    public int? CleanUpSlots { get; set; }
    public virtual IEnumerable<EventDish> EventDishes { get; set; }
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
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Parse($"{EventDate.ToShortDateString()} {EndTime}");
            int result = DateTime.Compare(date1, date2);
            bool isExpired = true;

            if (result <= 0)
            {
                isExpired = false;
            }

            if (string.IsNullOrEmpty(UserName) || isExpired)
            {
                link = $"<a href='#' class='disabled-link'>Edit</a>";
            }
            else if (!string.IsNullOrEmpty(UserName) && UserName.Equals(ContactEmail))
            {
                link = $"<a href='#' class='enabled-link' onclick='EditEvent({Id})'>Edit</a>";
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
