using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyTrackerMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class home : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Contacts",
                newName: "Role");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Senders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "LogoPic",
                table: "Senders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Receivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "LogoPic",
                table: "Receivers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Receivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "DeliveryBranches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeliveryBranches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactDetailValue",
                table: "ContactDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryDataTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivery_Receivers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Receivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivery_Senders_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Senders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_AddressId",
                table: "Receivers",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBranches_AddressId",
                table: "DeliveryBranches",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_ProductID",
                table: "Delivery",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_ReceiverId",
                table: "Delivery",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_SenderId",
                table: "Delivery",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBranches_Address_AddressId",
                table: "DeliveryBranches",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receivers_Address_AddressId",
                table: "Receivers",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBranches_Address_AddressId",
                table: "DeliveryBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_Receivers_Address_AddressId",
                table: "Receivers");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropIndex(
                name: "IX_Receivers_AddressId",
                table: "Receivers");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBranches_AddressId",
                table: "DeliveryBranches");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Senders");

            migrationBuilder.DropColumn(
                name: "LogoPic",
                table: "Senders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "LogoPic",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "DeliveryBranches");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeliveryBranches");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactDetailValue",
                table: "ContactDetails");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Contacts",
                newName: "Name");
        }
    }
}
