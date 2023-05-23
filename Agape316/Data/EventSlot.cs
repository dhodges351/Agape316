using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class EventSlot
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventDish> EventDishes { get; set; } = new List<EventDish>();
}
