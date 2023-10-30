using System;
using System.Collections.Generic;

namespace DbFirst.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateFounded { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();

    public virtual ICollection<League> IdLeagues { get; set; } = new List<League>();

    public virtual ICollection<Trainer> IdTrainers { get; set; } = new List<Trainer>();
}
