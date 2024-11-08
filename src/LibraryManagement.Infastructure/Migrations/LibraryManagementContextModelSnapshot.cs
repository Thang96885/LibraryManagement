﻿// <auto-generated />
using System;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagement.Infastructure.Migrations
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

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("LibraryManagement.Domain.BookReservationAggregate.BookReservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("bit");

                    b.Property<Guid>("PatronId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReservationExpirationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ReservationBookIds", (string)null);
                });

            modelBuilder.Entity("LibraryManagement.Domain.BorrowRecordAggregate.BorrowRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<Guid>("PatronId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReturnRecordId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("BorrowRecords", (string)null);
                });

            modelBuilder.Entity("LibraryManagement.Domain.GenreAggregate.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres", (string)null);
                });

            modelBuilder.Entity("LibraryManagement.Domain.PatronAggregate.Patron", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Patrons", (string)null);
                });

            modelBuilder.Entity("LibraryManagement.Domain.ReturnRecordAggregate.ReturnRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("BorrowRecordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("LateFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("PatronId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalFee")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("ReturnRecords", (string)null);
                });

            modelBuilder.Entity("LibraryManagement.Infastructure.Data.Identity.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PatronId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PatronId")
                        .IsUnique()
                        .HasFilter("[PatronId] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LibraryManagement.Domain.BookAggregate.Book", b =>
                {
                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.Entities.BookCopy", "BookCopies", b1 =>
                        {
                            b1.Property<string>("Id")
                                .HasMaxLength(13)
                                .HasColumnType("nvarchar(13)");

                            b1.Property<DateTime>("AcquisitionDate")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("PhysicalCondition")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("BookId");

                            b1.ToTable("BookCopy", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.ValueObjects.BookBorrowRecordId", "BorrowRecordIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("BorrowRecordId");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "BookId");

                            b1.HasIndex("BookId");

                            b1.ToTable("BookBorrowRecords", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.ValueObjects.BookGenreId", "GenreIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("BookGenreId");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "BookId");

                            b1.HasIndex("BookId");

                            b1.ToTable("BookGenres", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.ValueObjects.BookReservationId", "BookReservationId", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("BookReservationId");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "BookId");

                            b1.HasIndex("BookId");

                            b1.ToTable("BookReservations", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.BookAggregate.ValueObjects.BookReturnRecordId", "ReturnRecordIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("BookReturnRecordId");

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "BookId");

                            b1.HasIndex("BookId");

                            b1.ToTable("BookReturnRecords", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.Navigation("BookCopies");

                    b.Navigation("BookReservationId");

                    b.Navigation("BorrowRecordIds");

                    b.Navigation("GenreIds");

                    b.Navigation("ReturnRecordIds");
                });

            modelBuilder.Entity("LibraryManagement.Domain.BookReservationAggregate.BookReservation", b =>
                {
                    b.OwnsMany("LibraryManagement.Domain.BookReservationAggregate.ValueObjects.ReservationBookCopyId", "BookId", b1 =>
                        {
                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(450)")
                                .HasColumnName("BookId");

                            b1.Property<Guid>("BookReservationId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "BookReservationId");

                            b1.HasIndex("BookReservationId");

                            b1.ToTable("ReservationBookCopyId");

                            b1.WithOwner()
                                .HasForeignKey("BookReservationId");
                        });

                    b.Navigation("BookId");
                });

            modelBuilder.Entity("LibraryManagement.Domain.BorrowRecordAggregate.BorrowRecord", b =>
                {
                    b.OwnsMany("LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects.BorrowRecordBookId", "BookIds", b1 =>
                        {
                            b1.Property<Guid>("BookId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("BookId");

                            b1.Property<Guid>("BorrowRecordId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("BookCopyIds")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("BookCopyIds");

                            b1.HasKey("BookId", "BorrowRecordId");

                            b1.HasIndex("BorrowRecordId");

                            b1.ToTable("BorrowRecordBookId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BorrowRecordId");
                        });

                    b.Navigation("BookIds");
                });

            modelBuilder.Entity("LibraryManagement.Domain.GenreAggregate.Genre", b =>
                {
                    b.OwnsMany("LibraryManagement.Domain.GenreAggregate.ValueObjects.GenreBookId", "BookIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("BookId");

                            b1.Property<Guid>("GenreId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "GenreId");

                            b1.HasIndex("GenreId");

                            b1.ToTable("GenreBookIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("GenreId");
                        });

                    b.Navigation("BookIds");
                });

            modelBuilder.Entity("LibraryManagement.Domain.PatronAggregate.Patron", b =>
                {
                    b.OwnsMany("LibraryManagement.Domain.PatronAggregate.ValueObjects.PatronBorrowRecordId", "BorrowRecordIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("BorrowRecordId");

                            b1.Property<Guid>("PatronId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "PatronId");

                            b1.HasIndex("PatronId");

                            b1.ToTable("PatronBorrowRecordId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PatronId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.PatronAggregate.ValueObjects.PatronReservationId", "ReservationIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ReservationId");

                            b1.Property<Guid>("PatronId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "PatronId");

                            b1.HasIndex("PatronId");

                            b1.ToTable("PatronReservationId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PatronId");
                        });

                    b.OwnsMany("LibraryManagement.Domain.PatronAggregate.ValueObjects.PatronReturnRecordId", "ReturnRecordIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ReturnRecordId");

                            b1.Property<Guid>("PatronId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Value", "PatronId");

                            b1.HasIndex("PatronId");

                            b1.ToTable("PatronReturnRecordId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PatronId");
                        });

                    b.Navigation("BorrowRecordIds");

                    b.Navigation("ReservationIds");

                    b.Navigation("ReturnRecordIds");
                });

            modelBuilder.Entity("LibraryManagement.Domain.ReturnRecordAggregate.ReturnRecord", b =>
                {
                    b.OwnsMany("LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects.ReturnStatus", "BookReturnStatus", b1 =>
                        {
                            b1.Property<string>("BookId")
                                .HasMaxLength(13)
                                .HasColumnType("nvarchar(13)");

                            b1.Property<Guid>("ReturnRecordId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Condition")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BookId", "ReturnRecordId");

                            b1.HasIndex("ReturnRecordId");

                            b1.ToTable("BookReturnStatus", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ReturnRecordId");
                        });

                    b.Navigation("BookReturnStatus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LibraryManagement.Infastructure.Data.Identity.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LibraryManagement.Infastructure.Data.Identity.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagement.Infastructure.Data.Identity.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LibraryManagement.Infastructure.Data.Identity.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
