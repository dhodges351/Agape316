using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class EventDish
{
    public int Id { get; set; }

    public int SlotId { get; set; }

    public DateTime Created { get; set; }

    public DateTime EventDate { get; set; }

    public string Description { get; set; } = null!;

    public string? Notes { get; set; }

    public int Quantity { get; set; }

    public virtual EventSlot Slot { get; set; } = null!;
}
