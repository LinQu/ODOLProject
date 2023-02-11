using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODOL.Migrations
{
    /// <inheritdoc />
    public partial class addnilai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Penilaian",
                columns: table => new
                {
                    idPenilaian = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPembimbing = table.Column<int>(type: "int", nullable: false),
                    NIM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departemen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Periode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PengetahuanKerja = table.Column<int>(type: "int", nullable: false),
                    KualitasKertja = table.Column<int>(type: "int", nullable: false),
                    KecepatanKerja = table.Column<int>(type: "int", nullable: false),
                    SikapPerilaku = table.Column<int>(type: "int", nullable: false),
                    KreatifitasKerjaSama = table.Column<int>(type: "int", nullable: false),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    Beradaptasi = table.Column<int>(type: "int", nullable: false),
                    Ulasan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateadBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penilaian", x => x.idPenilaian);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Penilaian");

           
        }
    }
}
