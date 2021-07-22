using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EQ.DAL.Migrations
{
    public partial class ChangeRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Window_Service_ServiceId",
                schema: "service",
                table: "Window");

            migrationBuilder.DropForeignKey(
                name: "FK_Window_User_UserId",
                schema: "service",
                table: "Window");

            migrationBuilder.DropTable(
                name: "Request",
                schema: "service");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b8a495d4-05d9-4f4f-8653-bf6f1e7f4723"));

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c49c0336-0283-45f9-9f70-a2015b49315e"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "service",
                table: "Window",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                schema: "service",
                table: "Window",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceStatus = table.Column<int>(type: "int", nullable: false),
                    WindowId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "service",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Window_WindowId",
                        column: x => x.WindowId,
                        principalSchema: "service",
                        principalTable: "Window",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Role",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { new Guid("e0048231-9ffe-481e-8478-21df1cdd4bf3"), "admin" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("917f24c0-7d83-4976-8858-8641bcf0c0d7"), 0, "b57c5563-7c29-4b13-8f52-49c1e2511af7", "admin@eq.com", false, false, null, null, null, "D404559F602EAB6FD602AC7680DACBFAADD13630335E951F097AF3900E9DE176B6DB28512F2E000B9D04FBA5133E8B1C6E8DF59DB3A8AB9D60BE4B97CC9E81DB", null, false, new Guid("e0048231-9ffe-481e-8478-21df1cdd4bf3"), null, false, null });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ServiceId",
                schema: "service",
                table: "Ticket",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_WindowId",
                schema: "service",
                table: "Ticket",
                column: "WindowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Window_Service_ServiceId",
                schema: "service",
                table: "Window",
                column: "ServiceId",
                principalSchema: "service",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Window_User_UserId",
                schema: "service",
                table: "Window",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Window_Service_ServiceId",
                schema: "service",
                table: "Window");

            migrationBuilder.DropForeignKey(
                name: "FK_Window_User_UserId",
                schema: "service",
                table: "Window");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "service");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("917f24c0-7d83-4976-8858-8641bcf0c0d7"));

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("e0048231-9ffe-481e-8478-21df1cdd4bf3"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "service",
                table: "Window",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                schema: "service",
                table: "Window",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Request",
                schema: "service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceStatus = table.Column<int>(type: "int", nullable: false),
                    WindowId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_Window_WindowId",
                        column: x => x.WindowId,
                        principalSchema: "service",
                        principalTable: "Window",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Role",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { new Guid("c49c0336-0283-45f9-9f70-a2015b49315e"), "admin" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b8a495d4-05d9-4f4f-8653-bf6f1e7f4723"), 0, "851d9eed-2544-4bd7-b9cf-2fab151346b4", "admin@eq.com", false, false, null, null, null, "D404559F602EAB6FD602AC7680DACBFAADD13630335E951F097AF3900E9DE176B6DB28512F2E000B9D04FBA5133E8B1C6E8DF59DB3A8AB9D60BE4B97CC9E81DB", null, false, new Guid("c49c0336-0283-45f9-9f70-a2015b49315e"), null, false, null });

            migrationBuilder.CreateIndex(
                name: "IX_Request_WindowId",
                schema: "service",
                table: "Request",
                column: "WindowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Window_Service_ServiceId",
                schema: "service",
                table: "Window",
                column: "ServiceId",
                principalSchema: "service",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Window_User_UserId",
                schema: "service",
                table: "Window",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
