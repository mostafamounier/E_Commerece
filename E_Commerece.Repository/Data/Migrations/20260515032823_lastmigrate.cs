using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Repository.Data.Migrations
{
    public partial class lastmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "PaymentIntentId",
                table: "Orders",
                newName: "paymentIntentId");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "orderDate");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Street",
                table: "Orders",
                newName: "shippToAddress_Street");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_LName",
                table: "Orders",
                newName: "shippToAddress_LName");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_FName",
                table: "Orders",
                newName: "shippToAddress_FName");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Country",
                table: "Orders",
                newName: "shippToAddress_Country");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_City",
                table: "Orders",
                newName: "shippToAddress_City");

            migrationBuilder.RenameColumn(
                name: "BuryerEmail",
                table: "Orders",
                newName: "BuyerEmail");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "paymentIntentId",
                table: "Orders",
                newName: "PaymentIntentId");

            migrationBuilder.RenameColumn(
                name: "orderDate",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "shippToAddress_Street",
                table: "Orders",
                newName: "ShippingAddress_Street");

            migrationBuilder.RenameColumn(
                name: "shippToAddress_LName",
                table: "Orders",
                newName: "ShippingAddress_LName");

            migrationBuilder.RenameColumn(
                name: "shippToAddress_FName",
                table: "Orders",
                newName: "ShippingAddress_FName");

            migrationBuilder.RenameColumn(
                name: "shippToAddress_Country",
                table: "Orders",
                newName: "ShippingAddress_Country");

            migrationBuilder.RenameColumn(
                name: "shippToAddress_City",
                table: "Orders",
                newName: "ShippingAddress_City");

            migrationBuilder.RenameColumn(
                name: "BuyerEmail",
                table: "Orders",
                newName: "BuryerEmail");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
