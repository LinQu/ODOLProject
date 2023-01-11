using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODOL.Migrations
{
    /// <inheritdoc />
    public partial class buatMhs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mahasiswa",
                columns: table => new
                {
                    NIM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdPerusahaan = table.Column<int>(type: "int", nullable: false),
                    NamaMahasiswa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prodi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mahasiswa", x => x.NIM);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mahasiswa");
        }
    }
}
