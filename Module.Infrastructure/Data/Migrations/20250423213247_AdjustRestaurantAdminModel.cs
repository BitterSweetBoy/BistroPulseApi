using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRestaurantAdminModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Usuarios_Id",
                schema: "Restaurant",
                table: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                schema: "Restaurant",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_RestaurantId",
                schema: "Restaurant",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Restaurant",
                table: "Admins",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                schema: "Restaurant",
                table: "Admins",
                columns: new[] { "RestaurantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                schema: "Restaurant",
                table: "Admins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Usuarios_UserId",
                schema: "Restaurant",
                table: "Admins",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Usuarios_UserId",
                schema: "Restaurant",
                table: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                schema: "Restaurant",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserId",
                schema: "Restaurant",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Restaurant",
                table: "Admins",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                schema: "Restaurant",
                table: "Admins",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_RestaurantId",
                schema: "Restaurant",
                table: "Admins",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Usuarios_Id",
                schema: "Restaurant",
                table: "Admins",
                column: "Id",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
