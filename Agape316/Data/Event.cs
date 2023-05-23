using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class Event
{
    public Event()
    {
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Notes { get; set; }
    public string Location { get; set; }
    public string ContactEmail { get; set; }
    public DateTime Created { get; set; }
    public DateTime EventDate { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public virtual IEnumerable<EventDish> EventDishes { get; set; }
}
