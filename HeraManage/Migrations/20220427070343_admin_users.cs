using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeraManage.Migrations
{
    public partial class admin_users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", nullable: true),
                    verified = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");
        }
    }
}
