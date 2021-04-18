using Microsoft.EntityFrameworkCore.Migrations;

namespace Excel.Improter.Migrations
{
    public partial class init_ImpirtImprove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kordinat",
                table: "ExcelDatas",
                newName: "YenidenQiymetlendirme");

            migrationBuilder.AddColumn<string>(
                name: "BalansEhtiyyatlari2019",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hasilat",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasilatZamaniItkiler",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kesfiyyat",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Koordinat",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MineralXammalBazasiBerpasi",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoteberliyiTesdiqlenmeyen",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QaliqEhtiyyatlari2020",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenayeMenimsenilmesiSeviyyesi",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serh",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerhedlerinDeyishmesiVeDiger",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TesdiqEdilmishBalansCemi",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TesdiqOlunmaseBarede",
                table: "ExcelDatas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalansEhtiyyatlari2019",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "Hasilat",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "HasilatZamaniItkiler",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "Kesfiyyat",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "Koordinat",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "MineralXammalBazasiBerpasi",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "MoteberliyiTesdiqlenmeyen",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "QaliqEhtiyyatlari2020",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "SenayeMenimsenilmesiSeviyyesi",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "Serh",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "SerhedlerinDeyishmesiVeDiger",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "TesdiqEdilmishBalansCemi",
                table: "ExcelDatas");

            migrationBuilder.DropColumn(
                name: "TesdiqOlunmaseBarede",
                table: "ExcelDatas");

            migrationBuilder.RenameColumn(
                name: "YenidenQiymetlendirme",
                table: "ExcelDatas",
                newName: "Kordinat");
        }
    }
}
