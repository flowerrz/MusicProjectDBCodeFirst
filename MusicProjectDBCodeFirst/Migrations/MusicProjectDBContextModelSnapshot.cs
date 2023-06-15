﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicProjectDBCodeFirst.Data;

namespace MusicProjectDBCodeFirst.Migrations
{
    [DbContext(typeof(MusicProjectDBContext))]
    partial class MusicProjectDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlbumName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("CoverImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PerformerId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PerformerId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InstrumentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("InstrumentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Performer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BirthYear")
                        .HasColumnType("int");

                    b.Property<string>("PerformerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RecordLabelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecordLabelId");

                    b.ToTable("Performers");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.PerformerInstrument", b =>
                {
                    b.Property<int?>("InstrumentId")
                        .HasColumnType("int");

                    b.Property<int?>("PerformerId")
                        .HasColumnType("int");

                    b.HasKey("InstrumentId", "PerformerId");

                    b.HasIndex("PerformerId");

                    b.ToTable("PerformersInstruments");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.RecordLabel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryName")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LabelName")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("RecordLabels");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<double>("SongDuration")
                        .HasColumnType("float");

                    b.Property<string>("SongName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Album", b =>
                {
                    b.HasOne("MusicProjectDBCodeFirst.Data.Models.Performer", "Performer")
                        .WithMany("Albums")
                        .HasForeignKey("PerformerId")
                        .HasConstraintName("FK_Performer_Album");

                    b.Navigation("Performer");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Performer", b =>
                {
                    b.HasOne("MusicProjectDBCodeFirst.Data.Models.RecordLabel", "RecordLabel")
                        .WithMany("Performers")
                        .HasForeignKey("RecordLabelId")
                        .HasConstraintName("FK_Performer_Label")
                        .IsRequired();

                    b.Navigation("RecordLabel");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.PerformerInstrument", b =>
                {
                    b.HasOne("MusicProjectDBCodeFirst.Data.Models.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId")
                        .HasConstraintName("FK_Instrument_PerformerInstrument")
                        .IsRequired();

                    b.HasOne("MusicProjectDBCodeFirst.Data.Models.Performer", "Performer")
                        .WithMany()
                        .HasForeignKey("PerformerId")
                        .HasConstraintName("FK_Performer_PerformerInstrument")
                        .IsRequired();

                    b.Navigation("Instrument");

                    b.Navigation("Performer");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Song", b =>
                {
                    b.HasOne("MusicProjectDBCodeFirst.Data.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .HasConstraintName("FK_Album_Song");

                    b.Navigation("Album");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.Performer", b =>
                {
                    b.Navigation("Albums");
                });

            modelBuilder.Entity("MusicProjectDBCodeFirst.Data.Models.RecordLabel", b =>
                {
                    b.Navigation("Performers");
                });
#pragma warning restore 612, 618
        }
    }
}