using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infastructure.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookReservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReservation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    NumberOfCopy = table.Column<int>(type: "int", nullable: false),
                    NumberAvailable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patrons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReturnRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BorrowRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LateFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationBookCopyId",
                columns: table => new
                {
                    BookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationBookCopyId", x => new { x.BookId, x.BookReservationId });
                    table.ForeignKey(
                        name: "FK_ReservationBookCopyId_BookReservation_BookReservationId",
                        column: x => x.BookReservationId,
                        principalTable: "BookReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowRecords",
                columns: table => new
                {
                    BorrowRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowRecords", x => new { x.BorrowRecordId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookBorrowRecords_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCopy",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    AcquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysicalCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCopy_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    BookGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.BookGenreId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookReservations",
                columns: table => new
                {
                    BookReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReservations", x => new { x.BookReservationId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookReservations_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookReturnRecords",
                columns: table => new
                {
                    BookReturnRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReturnRecords", x => new { x.BookReturnRecordId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookReturnRecords_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRecordBookId",
                columns: table => new
                {
                    BookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BorrowRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRecordBookId", x => new { x.BookId, x.BorrowRecordId });
                    table.ForeignKey(
                        name: "FK_BorrowRecordBookId_BorrowRecords_BorrowRecordId",
                        column: x => x.BorrowRecordId,
                        principalTable: "BorrowRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreBookIds",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreBookIds", x => new { x.BookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GenreBookIds_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatronBorrowRecordId",
                columns: table => new
                {
                    BorrowRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatronBorrowRecordId", x => new { x.BorrowRecordId, x.PatronId });
                    table.ForeignKey(
                        name: "FK_PatronBorrowRecordId_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatronReservationId",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatronReservationId", x => new { x.ReservationId, x.PatronId });
                    table.ForeignKey(
                        name: "FK_PatronReservationId_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatronReturnRecordId",
                columns: table => new
                {
                    ReturnRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatronReturnRecordId", x => new { x.ReturnRecordId, x.PatronId });
                    table.ForeignKey(
                        name: "FK_PatronReturnRecordId_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookReturnStatus",
                columns: table => new
                {
                    ReturnRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReturnStatus", x => new { x.ReturnRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_BookReturnStatus_ReturnRecords_ReturnRecordId",
                        column: x => x.ReturnRecordId,
                        principalTable: "ReturnRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowRecords_BookId",
                table: "BookBorrowRecords",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_BookId",
                table: "BookCopy",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_BookId",
                table: "BookReservations",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReturnRecords_BookId",
                table: "BookReturnRecords",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecordBookId_BorrowRecordId",
                table: "BorrowRecordBookId",
                column: "BorrowRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreBookIds_GenreId",
                table: "GenreBookIds",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatronBorrowRecordId_PatronId",
                table: "PatronBorrowRecordId",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_PatronReservationId_PatronId",
                table: "PatronReservationId",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_PatronReturnRecordId_PatronId",
                table: "PatronReturnRecordId",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationBookCopyId_BookReservationId",
                table: "ReservationBookCopyId",
                column: "BookReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBorrowRecords");

            migrationBuilder.DropTable(
                name: "BookCopy");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "BookReservations");

            migrationBuilder.DropTable(
                name: "BookReturnRecords");

            migrationBuilder.DropTable(
                name: "BookReturnStatus");

            migrationBuilder.DropTable(
                name: "BorrowRecordBookId");

            migrationBuilder.DropTable(
                name: "GenreBookIds");

            migrationBuilder.DropTable(
                name: "PatronBorrowRecordId");

            migrationBuilder.DropTable(
                name: "PatronReservationId");

            migrationBuilder.DropTable(
                name: "PatronReturnRecordId");

            migrationBuilder.DropTable(
                name: "ReservationBookCopyId");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "ReturnRecords");

            migrationBuilder.DropTable(
                name: "BorrowRecords");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Patrons");

            migrationBuilder.DropTable(
                name: "BookReservation");
        }
    }
}
