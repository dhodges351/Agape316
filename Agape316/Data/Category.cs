using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryDescriptionId { get; set; }

    public virtual CategoryDescription CategoryDescription { get; set; } = null!;
}
