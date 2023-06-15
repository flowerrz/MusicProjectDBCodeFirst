using System;
using System.Collections.Generic;
using System.Text;
using MusicProjectDBCodeFirst.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicProjectDBCodeFirst.Data
{
    public class MusicProjectDBContext : DbContext
    {
        public MusicProjectDBContext()
        {

        }

        public MusicProjectDBContext(DbContextOptions<MusicProjectDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerformerInstrument>(entity =>
            {
                entity.HasKey(nameof(PerformerInstrument.InstrumentId), nameof(PerformerInstrument.PerformerId));

                entity.HasOne(d => d.Performer)
                    .WithMany()
                    .HasForeignKey(d => d.PerformerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Performer_PerformerInstrument");

                entity.HasOne(d => d.Instrument)
                    .WithMany()
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instrument_PerformerInstrument");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id");

                entity.Property(e => e.SongName).HasMaxLength(50);

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Album_Song");
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasColumnName("Id");

                entity.Property(e => e.AlbumName)
                      .HasMaxLength(50)
                      .IsUnicode(true);

                entity.HasOne(e => e.Performer)
                      .WithMany(p => p.Albums)
                      .HasForeignKey(d => d.PerformerId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Performer_Album");
            });

            modelBuilder.Entity<Performer>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasColumnName("Id");

                entity.Property(p => p.PerformerName)
                      .HasMaxLength(50)
                      .IsUnicode(true);

                entity.HasOne(e => e.RecordLabel)
                      .WithMany(p => p.Performers)
                      .HasForeignKey(d => d.RecordLabelId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Performer_Label");
            });

            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.InstrumentName)
                      .HasMaxLength(50)
                      .IsUnicode(true);

                entity.Property(e => e.InstrumentType)
                      .HasMaxLength(50)
                      .IsUnicode(true);
            });

            modelBuilder.Entity<RecordLabel>(entity =>
           {
               entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

               entity.Property(e => e.LabelName)
                      .HasMaxLength(50)
                      .IsUnicode(true);

               entity.Property(e => e.CountryName)
                      .HasMaxLength(50)
                      .IsUnicode(true);
           });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<RecordLabel> RecordLabels { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<PerformerInstrument> PerformersInstruments { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
