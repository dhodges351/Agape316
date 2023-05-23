using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Notes { get; set; }

    public string Location { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime EventDate { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }
}
