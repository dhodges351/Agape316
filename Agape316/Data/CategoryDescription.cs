using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class CategoryDescription
{
    public int Id { get; set; }

    public string? CategoryDescription1 { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
