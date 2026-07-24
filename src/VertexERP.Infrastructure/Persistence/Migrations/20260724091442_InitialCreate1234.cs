using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VertexERP.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "identity",
                table: "Users",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                schema: "identity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                schema: "identity",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "identity",
                table: "Users",
                newName: "FullName");
        }
    }
}
