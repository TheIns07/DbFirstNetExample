using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Models;

public partial class LeagueDatabaseContext : DbContext
{
    public LeagueDatabaseContext()
    {
    }

    public LeagueDatabaseContext(DbContextOptions<LeagueDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<Register> Registers { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=LeagueDatabase;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Record>(entity =>
        {
            entity.HasIndex(e => e.RegisterId, "IX_Records_RegisterId");

            entity.HasIndex(e => e.TeamId, "IX_Records_TeamId");

            entity.HasOne(d => d.Register).WithMany(p => p.Records).HasForeignKey(d => d.RegisterId);

            entity.HasOne(d => d.Team).WithMany(p => p.Records).HasForeignKey(d => d.TeamId);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasMany(d => d.IdLeagues).WithMany(p => p.IdTeams)
                .UsingEntity<Dictionary<string, object>>(
                    "TeamLeague",
                    r => r.HasOne<League>().WithMany().HasForeignKey("IdLeague"),
                    l => l.HasOne<Team>().WithMany().HasForeignKey("IdTeam"),
                    j =>
                    {
                        j.HasKey("IdTeam", "IdLeague");
                        j.ToTable("TeamLeagues");
                        j.HasIndex(new[] { "IdLeague" }, "IX_TeamLeagues_IdLeague");
                    });

            entity.HasMany(d => d.IdTrainers).WithMany(p => p.IdTeams)
                .UsingEntity<Dictionary<string, object>>(
                    "TeamTrainer",
                    r => r.HasOne<Trainer>().WithMany().HasForeignKey("IdTrainer"),
                    l => l.HasOne<Team>().WithMany().HasForeignKey("IdTeam"),
                    j =>
                    {
                        j.HasKey("IdTeam", "IdTrainer");
                        j.ToTable("TeamTrainers");
                        j.HasIndex(new[] { "IdTrainer" }, "IX_TeamTrainers_IdTrainer");
                    });
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasIndex(e => e.StateId, "IX_Trainers_StateId");

            entity.HasOne(d => d.State).WithMany(p => p.Trainers).HasForeignKey(d => d.StateId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
