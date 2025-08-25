using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surix.Api.Migrations
{
    /// <inheritdoc />
    public partial class MudandoDadosParaDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "oddB",
                table: "Sures",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<decimal>(
                name: "oddA",
                table: "Sures",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<decimal>(
                name: "ROI",
                table: "Sures",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Lucro",
                table: "Sures",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "oddB",
                table: "Sures",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<double>(
                name: "oddA",
                table: "Sures",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ROI",
                table: "Sures",
                type: "double",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Lucro",
                table: "Sures",
                type: "double",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);
        }
    }
}
