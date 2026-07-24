using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VertexERP.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRoles",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roles",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "RolePermissions",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshTokens",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "Permissions",
                newSchema: "identity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "identity",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "identity",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "identity",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                schema: "identity",
                newName: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                schema: "identity",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "Permissions",
                schema: "identity",
                newName: "Permissions");
        }
    }
}
