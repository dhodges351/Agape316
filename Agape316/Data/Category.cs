using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Active { get; set; }
    public bool Private { get; set; } 
    public IEnumerable<Category>? Categories { get; set;}
}
