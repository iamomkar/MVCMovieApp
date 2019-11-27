using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieAppSQL.Models
{
    public partial class MovieAppDBContext : DbContext
    {
        public MovieAppDBContext()
        {
        }

        public MovieAppDBContext(DbContextOptions<MovieAppDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=FSIND-LT-18\\SQLEXPRESS;Database=MovieAppDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.MovieId)
                    .HasName("PK_MovieId");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MovieName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseYear)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
