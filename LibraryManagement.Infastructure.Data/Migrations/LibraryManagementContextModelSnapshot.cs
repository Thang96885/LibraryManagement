﻿// <auto-generated />
using System;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagement.Infastructure.Data.Migrations
{
    [DbContext(typeof(LibraryManagementContext))]
    partial class LibraryManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LibraryManagement.Domain.BookAggregate.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberAvailable")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfCopy")
                        .HasColumnType("int");

                    b.Property<int>("PageCount")
                        .HasColumnType("int");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("int");

                    b.Property<string>("PublisherName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("LibraryManagement.Domain.BookAggregate.Book", b =>
                {
                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.ValueObjects.BookBorrowRecordId", "BorrowRecordIds", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("BookId");

                            b1.ToTable("BorrowRecords", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.ValueObjects.BookGenreId", "GenreIds", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("BookId");

                            b1.ToTable("BookGenres", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.ValueObjects.BookReservationId", "BookReservationId", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("BookId");

                            b1.ToTable("BookReservations", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.ValueObjects.BookReturnRecordId", "ReturnRecordIds", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("BookId");

                            b1.ToTable("ReturnRecords", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.Navigation("BookReservationId");

                    b.Navigation("BorrowRecordIds");

                    b.Navigation("GenreIds");

                    b.Navigation("ReturnRecordIds");
                });
#pragma warning restore 612, 618
        }
    }
}