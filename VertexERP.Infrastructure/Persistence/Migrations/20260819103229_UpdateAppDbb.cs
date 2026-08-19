using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VertexERP.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppDbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Stocks",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 18,
                oldScale: 2,
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ReservedQuantity",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedQuantity",
                table: "Stocks");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Stocks",
                type: "int",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldDefaultValue: 0m);
        }
    }
}
