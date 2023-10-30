using System;
using System.Collections.Generic;

namespace DbFirst.Models;

public partial class Record
{
    public int Id { get; set; }

    public string TitleRecord { get; set; } = null!;

    public string TextRecord { get; set; } = null!;

    public DateTime DateRecorded { get; set; }

    public int RegisterId { get; set; }

    public int TeamId { get; set; }

    public virtual Register Register { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
