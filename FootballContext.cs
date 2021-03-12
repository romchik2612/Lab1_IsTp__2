using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lab1_IsTp__2
{
    public partial class FootballContext : DbContext
    {
        public FootballContext()
        {
        }

        public FootballContext(DbContextOptions<FootballContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<ClubTrophy> ClubTrophies { get; set; }
        public virtual DbSet<Cup> Cups { get; set; }
        public virtual DbSet<EuroCup> EuroCups { get; set; }
        public virtual DbSet<Footboller> Footbollers { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<National> Nationals { get; set; }
        public virtual DbSet<NationalCup> NationalCups { get; set; }
        public virtual DbSet<NationalTrophy> NationalTrophies { get; set; }
        public virtual DbSet<PlayerOfTheYear> PlayerOfTheYears { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-I5ILJ0P\\SQLEXPRESS; Database= Football; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Club>(entity =>
            {
                entity.ToTable("Club");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CupId).HasColumnName("Cup_id");

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Date_of_Birth")
                    .IsFixedLength();

                entity.Property(e => e.EuroCupId).HasColumnName("EuroCup_id");

                entity.Property(e => e.LeagueId).HasColumnName("League_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.NationalId).HasColumnName("National_id");

                entity.Property(e => e.PlayersNumber).HasColumnName("Players_Number");

                entity.Property(e => e.PositionInLeague).HasColumnName("Position_in_league");

                entity.Property(e => e.TrophiesNumber).HasColumnName("Trophies_Number");

                entity.HasOne(d => d.Cup)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.CupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Club_Cup");

                entity.HasOne(d => d.EuroCup)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.EuroCupId)
                    .HasConstraintName("FK_Club_EuroCup");

                entity.HasOne(d => d.League)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.LeagueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Club_League");

                entity.HasOne(d => d.National)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Club_National");
            });

            modelBuilder.Entity<ClubTrophy>(entity =>
            {
                entity.ToTable("Club_Trophy");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Cup>(entity =>
            {
                entity.ToTable("Cup");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClubsNumber).HasColumnName("Clubs_Number");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<EuroCup>(entity =>
            {
                entity.ToTable("EuroCup");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClubsNumber).HasColumnName("Clubs_Number");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Footboller>(entity =>
            {
                entity.ToTable("Footboller");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssistsNumber).HasColumnName("Assists_Number");

                entity.Property(e => e.ClubId).HasColumnName("Club_id");

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Date_of_Birth")
                    .IsFixedLength();

                entity.Property(e => e.GoalsNumber).HasColumnName("Goals_Number");

                entity.Property(e => e.MatchesNumber).HasColumnName("Matches_Number");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.NationalId).HasColumnName("National_id");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Sourname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Footbollers)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Footboller_Club");

                entity.HasOne(d => d.National)
                    .WithMany(p => p.Footbollers)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Footboller_National");
            });

            modelBuilder.Entity<League>(entity =>
            {
                entity.ToTable("League");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClubsNumber).HasColumnName("Clubs_Number");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<National>(entity =>
            {
                entity.ToTable("National");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.NationalCupId).HasColumnName("NationalCup_id");

                entity.Property(e => e.TrophiesNumber).HasColumnName("Trophies_Number");

                entity.HasOne(d => d.NationalCup)
                    .WithMany(p => p.Nationals)
                    .HasForeignKey(d => d.NationalCupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_National_NationalCup");
            });

            modelBuilder.Entity<NationalCup>(entity =>
            {
                entity.ToTable("NationalCup");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.TeamsNumber).HasColumnName("Teams_Number");
            });

            modelBuilder.Entity<NationalTrophy>(entity =>
            {
                entity.ToTable("National_Trophy");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PlayerOfTheYear>(entity =>
            {
                entity.ToTable("Player_of_the_year");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.WinnerId).HasColumnName("Winner_id");

                entity.HasOne(d => d.Winner)
                    .WithMany(p => p.PlayerOfTheYears)
                    .HasForeignKey(d => d.WinnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Player_of_the_year_Footboller");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
