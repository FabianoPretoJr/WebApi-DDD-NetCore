using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class NovoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("31792958-2ef4-4444-8955-a97b176a62ef"), new DateTime(2021, 1, 13, 14, 23, 47, 340, DateTimeKind.Local).AddTicks(1326), "admin@mail.com", "Administrador", new DateTime(2021, 1, 13, 14, 23, 47, 341, DateTimeKind.Local).AddTicks(4252) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("31792958-2ef4-4444-8955-a97b176a62ef"));
        }
    }
}
