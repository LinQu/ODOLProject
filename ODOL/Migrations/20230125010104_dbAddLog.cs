using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODOL.Migrations
{
    /// <inheritdoc />
    public partial class dbAddLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "LogBook",
                columns: table => new
                {
                    idLogBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPembimbing = table.Column<int>(type: "int", nullable: false),
                    NIM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departemen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kegiatan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RincianKegiatan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JamStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JamEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tanggal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ulasan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogBook", x => x.idLogBook);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogBook");

        }
    }
}
