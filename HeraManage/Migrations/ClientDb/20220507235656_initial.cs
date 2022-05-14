using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeraManage.Migrations.ClientDb
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountPoints",
                columns: table => new
                {
                    uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPoints", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "ClientAccounts",
                columns: table => new
                {
                    uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAccounts", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RSAIdNumber = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<int>(type: "int", nullable: false),
                    verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.uid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPoints");

            migrationBuilder.DropTable(
                name: "ClientAccounts");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
