using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyTrackerMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Receivers_ReceiverId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Senders_SenderId",
                table: "Contacts");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Receivers_ReceiverId",
                table: "Contacts",
                column: "ReceiverId",
                principalTable: "Receivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Senders_SenderId",
                table: "Contacts",
                column: "SenderId",
                principalTable: "Senders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Receivers_ReceiverId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Senders_SenderId",
                table: "Contacts");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Receivers_ReceiverId",
                table: "Contacts",
                column: "ReceiverId",
                principalTable: "Receivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Senders_SenderId",
                table: "Contacts",
                column: "SenderId",
                principalTable: "Senders",
                principalColumn: "Id");
        }
    }
}
