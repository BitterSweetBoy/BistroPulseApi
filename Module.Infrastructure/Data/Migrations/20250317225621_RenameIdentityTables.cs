using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                schema: "Logs",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Admins_AspNetUsers_Id",
                schema: "Restaurant",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_AspNetUsers_UserId",
                schema: "Logs",
                table: "AttendanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_Id",
                schema: "Users",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Riders_AspNetUsers_Id",
                schema: "Riders",
                table: "Riders");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionTickets_AspNetUsers_UserId",
                schema: "Auth",
                table: "SessionTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportUsers_AspNetUsers_Id",
                schema: "Users",
                table: "SupportUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "UsuarioTokens",
                newSchema: "Auth");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Usuarios",
                newSchema: "Auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UsuarioRoles",
                newSchema: "Auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UsuarioLogins",
                newSchema: "Auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UsuarioClaims",
                newSchema: "Auth");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Roles",
                newSchema: "Auth");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "RolClaims",
                newSchema: "Auth");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Auth",
                table: "UsuarioRoles",
                newName: "IX_UsuarioRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Auth",
                table: "UsuarioLogins",
                newName: "IX_UsuarioLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Auth",
                table: "UsuarioClaims",
                newName: "IX_UsuarioClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Auth",
                table: "RolClaims",
                newName: "IX_RolClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioTokens",
                schema: "Auth",
                table: "UsuarioTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                schema: "Auth",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRoles",
                schema: "Auth",
                table: "UsuarioRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioLogins",
                schema: "Auth",
                table: "UsuarioLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioClaims",
                schema: "Auth",
                table: "UsuarioClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "Auth",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolClaims",
                schema: "Auth",
                table: "RolClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Usuarios_UserId",
                schema: "Logs",
                table: "ActivityLogs",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Usuarios_Id",
                schema: "Restaurant",
                table: "Admins",
                column: "Id",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_Usuarios_UserId",
                schema: "Logs",
                table: "AttendanceRecords",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Usuarios_Id",
                schema: "Users",
                table: "Customers",
                column: "Id",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Riders_Usuarios_Id",
                schema: "Riders",
                table: "Riders",
                column: "Id",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolClaims_Roles_RoleId",
                schema: "Auth",
                table: "RolClaims",
                column: "RoleId",
                principalSchema: "Auth",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionTickets_Usuarios_UserId",
                schema: "Auth",
                table: "SessionTickets",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportUsers_Usuarios_Id",
                schema: "Users",
                table: "SupportUsers",
                column: "Id",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioClaims_Usuarios_UserId",
                schema: "Auth",
                table: "UsuarioClaims",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioLogins_Usuarios_UserId",
                schema: "Auth",
                table: "UsuarioLogins",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRoles_Roles_RoleId",
                schema: "Auth",
                table: "UsuarioRoles",
                column: "RoleId",
                principalSchema: "Auth",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRoles_Usuarios_UserId",
                schema: "Auth",
                table: "UsuarioRoles",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTokens_Usuarios_UserId",
                schema: "Auth",
                table: "UsuarioTokens",
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
                name: "FK_ActivityLogs_Usuarios_UserId",
                schema: "Logs",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Usuarios_Id",
                schema: "Restaurant",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_Usuarios_UserId",
                schema: "Logs",
                table: "AttendanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Usuarios_Id",
                schema: "Users",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Riders_Usuarios_Id",
                schema: "Riders",
                table: "Riders");

            migrationBuilder.DropForeignKey(
                name: "FK_RolClaims_Roles_RoleId",
                schema: "Auth",
                table: "RolClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionTickets_Usuarios_UserId",
                schema: "Auth",
                table: "SessionTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportUsers_Usuarios_Id",
                schema: "Users",
                table: "SupportUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioClaims_Usuarios_UserId",
                schema: "Auth",
                table: "UsuarioClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioLogins_Usuarios_UserId",
                schema: "Auth",
                table: "UsuarioLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRoles_Roles_RoleId",
                schema: "Auth",
                table: "UsuarioRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRoles_Usuarios_UserId",
                schema: "Auth",
                table: "UsuarioRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTokens_Usuarios_UserId",
                schema: "Auth",
                table: "UsuarioTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioTokens",
                schema: "Auth",
                table: "UsuarioTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                schema: "Auth",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRoles",
                schema: "Auth",
                table: "UsuarioRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioLogins",
                schema: "Auth",
                table: "UsuarioLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioClaims",
                schema: "Auth",
                table: "UsuarioClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "Auth",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolClaims",
                schema: "Auth",
                table: "RolClaims");

            migrationBuilder.RenameTable(
                name: "UsuarioTokens",
                schema: "Auth",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                schema: "Auth",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UsuarioRoles",
                schema: "Auth",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UsuarioLogins",
                schema: "Auth",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UsuarioClaims",
                schema: "Auth",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Auth",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "RolClaims",
                schema: "Auth",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RolClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                schema: "Logs",
                table: "ActivityLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_AspNetUsers_Id",
                schema: "Restaurant",
                table: "Admins",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_AspNetUsers_UserId",
                schema: "Logs",
                table: "AttendanceRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_Id",
                schema: "Users",
                table: "Customers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Riders_AspNetUsers_Id",
                schema: "Riders",
                table: "Riders",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionTickets_AspNetUsers_UserId",
                schema: "Auth",
                table: "SessionTickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportUsers_AspNetUsers_Id",
                schema: "Users",
                table: "SupportUsers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
