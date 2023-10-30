using System;
using System.Collections.Generic;

namespace DbFirst.Models;

public partial class Trainer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Stadium { get; set; } = null!;

    public int StateId { get; set; }

    public string Field { get; set; } = null!;

    public virtual State State { get; set; } = null!;

    public virtual ICollection<Team> IdTeams { get; set; } = new List<Team>();
}
