using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OKEX.Auto.Core.ORM.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseCurrency",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    currency = table.Column<string>(maxLength: 256, nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    can_deposit = table.Column<string>(maxLength: 256, nullable: true),
                    can_withdraw = table.Column<string>(maxLength: 256, nullable: true),
                    min_withdrawal = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCurrency", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseCurrency");
        }
    }
}
