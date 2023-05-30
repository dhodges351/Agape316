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
}
