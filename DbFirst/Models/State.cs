using System;
using System.Collections.Generic;

namespace DbFirst.Models;

public partial class State
{
    public int Id { get; set; }

    public string NameBorough { get; set; } = null!;

    public string NameCity { get; set; } = null!;

    public string NameState { get; set; } = null!;

    public string CountryState { get; set; } = null!;

    public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
}
