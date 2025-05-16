using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Models
{
    public partial class CinemaDBContext : DbContext
    {
        public CinemaDBContext()
        {
        }

        public CinemaDBContext(DbContextOptions<CinemaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Screening> Screenings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        // Remove or comment out this method:
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=LAPTOP-KAQ2H7J2;Database=db_cinema;Trusted_Connection=True;Trust Server Certificate=True");
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hall>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__halls__3213E83FCFD57C07");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__movies__3213E83F8BD5869E");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__reservat__3213E83F3AF22C3B");

                entity.HasOne(d => d.Costumer).WithMany(p => p.Reservations)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reservations_users");

                entity.HasOne(d => d.Scr).WithMany(p => p.Reservations).HasConstraintName("FK_reservations_screenings");
            });

            modelBuilder.Entity<Screening>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__screenin__3213E83FBF8903A1");

                entity.HasOne(d => d.Hall).WithMany(p => p.Screenings)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_screenings_halls");

                entity.HasOne(d => d.Movie).WithMany(p => p.Screenings)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_screenings_movies");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__users__3213E83F3487FF8E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
