using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetcoreapi_cake_shop.Migrations
{
    public partial class shippingmethodfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerKm",
                table: "ShippingMethods",
                newName: "InitialCharge");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ShippingMethods",
                newName: "AdditionalCharge");

            migrationBuilder.AddColumn<double>(
                name: "InitialDistance",
                table: "ShippingMethods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialDistance",
                table: "ShippingMethods");

            migrationBuilder.RenameColumn(
                name: "InitialCharge",
                table: "ShippingMethods",
                newName: "PricePerKm");

            migrationBuilder.RenameColumn(
                name: "AdditionalCharge",
                table: "ShippingMethods",
                newName: "Price");
        }
    }
}
