using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class deliveryTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TimeSlot",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DeliveryDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliverуDateTimeSlots",
                columns: table => new
                {
                    DeliveryDateId = table.Column<int>(type: "int", nullable: false),
                    TimeSlotId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaximumOrders = table.Column<int>(type: "int", nullable: true),
                    MadeOrders = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverуDateTimeSlots", x => new { x.DeliveryDateId, x.TimeSlotId });
                    table.ForeignKey(
                        name: "FK_DeliverуDateTimeSlots_DeliveryDates_DeliveryDateId",
                        column: x => x.DeliveryDateId,
                        principalTable: "DeliveryDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliverуDateTimeSlots_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliverуDateTimeSlots_TimeSlotId",
                table: "DeliverуDateTimeSlots",
                column: "TimeSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliverуDateTimeSlots");

            migrationBuilder.DropTable(
                name: "DeliveryDates");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "RefreshToken", "Username" },
                values: new object[] { 1, new byte[] { 61, 170, 24, 143, 182, 202, 54, 197, 68, 216, 222, 112, 22, 195, 214, 220, 211, 60, 226, 87, 229, 242, 224, 152, 245, 245, 171, 78, 175, 197, 167, 202, 9, 189, 230, 111, 251, 241, 27, 188, 62, 33, 116, 168, 159, 102, 52, 119, 114, 221, 226, 109, 184, 95, 127, 243, 26, 37, 151, 53, 88, 149, 114, 33 }, new byte[] { 110, 82, 187, 46, 152, 172, 182, 23, 24, 160, 103, 210, 67, 142, 223, 69, 117, 205, 173, 216, 148, 27, 202, 222, 86, 254, 244, 158, 61, 197, 212, 74, 189, 211, 176, 43, 146, 177, 2, 177, 214, 72, 131, 210, 80, 99, 124, 50, 56, 74, 113, 215, 24, 213, 251, 210, 213, 8, 173, 167, 92, 184, 70, 228, 170, 30, 14, 175, 94, 36, 123, 214, 142, 166, 216, 93, 47, 110, 138, 87, 35, 83, 22, 145, 43, 63, 166, 117, 201, 219, 104, 31, 195, 78, 225, 204, 160, 252, 177, 103, 130, 255, 32, 21, 185, 89, 161, 63, 229, 86, 185, 92, 109, 175, 101, 57, 139, 155, 255, 91, 97, 121, 151, 188, 224, 184, 208, 222 }, null, "lizaveta.razumovich@gmail.com" });
        }
    }
}
