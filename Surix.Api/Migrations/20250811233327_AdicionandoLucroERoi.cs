using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surix.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoLucroERoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "oddB",
                table: "Sures",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "oddA",
                table: "Sures",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Stake",
                table: "Sures",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "Lucro",
                table: "Sures",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ROI",
                table: "Sures",
                type: "double",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lucro",
                table: "Sures");

            migrationBuilder.DropColumn(
                name: "ROI",
                table: "Sures");

            migrationBuilder.AlterColumn<float>(
                name: "oddB",
                table: "Sures",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "oddA",
                table: "Sures",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Stake",
                table: "Sures",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
