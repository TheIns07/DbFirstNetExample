using System;
using System.Collections.Generic;

namespace DbFirst.Models;

public partial class Register
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
