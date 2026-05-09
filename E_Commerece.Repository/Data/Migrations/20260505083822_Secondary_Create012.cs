using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Repository.Data.Migrations
{
    public partial class Secondary_Create012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "deliveryMethodId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int?");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "deliveryMethodId",
                table: "Orders",
                type: "int?",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
