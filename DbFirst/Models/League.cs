using System;
using System.Collections.Generic;

namespace DbFirst.Models;

public partial class League
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Team> IdTeams { get; set; } = new List<Team>();
}
