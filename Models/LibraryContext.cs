using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PonfeLibrary.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookAuth> BookAuth { get; set; }
        public virtual DbSet<BookGen> BookGen { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_AName")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Nationality).IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn)
                    .HasName("UQ_ISBN")
                    .IsUnique();

                entity.Property(e => e.Isbn).IsUnicode(false);

                entity.Property(e => e.Subtitle).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.Pub)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.PubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Publisher");
            });

            modelBuilder.Entity<BookAuth>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.AuthId })
                    .HasName("PK_Book_Au");

                entity.HasOne(d => d.Auth)
                    .WithMany(p => p.BookAuth)
                    .HasForeignKey(d => d.AuthId)
                    .HasConstraintName("FK_AuthG");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookAuth)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BookA");
            });

            modelBuilder.Entity<BookGen>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.GenId });

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookGen)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BookG");

                entity.HasOne(d => d.Gen)
                    .WithMany(p => p.BookGen)
                    .HasForeignKey(d => d.GenId)
                    .HasConstraintName("FK_GenG");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_GName")
                    .IsUnique();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_PName")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
