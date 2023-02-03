using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class orderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 61, 170, 24, 143, 182, 202, 54, 197, 68, 216, 222, 112, 22, 195, 214, 220, 211, 60, 226, 87, 229, 242, 224, 152, 245, 245, 171, 78, 175, 197, 167, 202, 9, 189, 230, 111, 251, 241, 27, 188, 62, 33, 116, 168, 159, 102, 52, 119, 114, 221, 226, 109, 184, 95, 127, 243, 26, 37, 151, 53, 88, 149, 114, 33 }, new byte[] { 110, 82, 187, 46, 152, 172, 182, 23, 24, 160, 103, 210, 67, 142, 223, 69, 117, 205, 173, 216, 148, 27, 202, 222, 86, 254, 244, 158, 61, 197, 212, 74, 189, 211, 176, 43, 146, 177, 2, 177, 214, 72, 131, 210, 80, 99, 124, 50, 56, 74, 113, 215, 24, 213, 251, 210, 213, 8, 173, 167, 92, 184, 70, 228, 170, 30, 14, 175, 94, 36, 123, 214, 142, 166, 216, 93, 47, 110, 138, 87, 35, 83, 22, 145, 43, 63, 166, 117, 201, 219, 104, 31, 195, 78, 225, 204, 160, 252, 177, 103, 130, 255, 32, 21, 185, 89, 161, 63, 229, 86, 185, 92, 109, 175, 101, 57, 139, 155, 255, 91, 97, 121, 151, 188, 224, 184, 208, 222 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "OrderItems");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 214, 87, 236, 96, 205, 224, 71, 127, 91, 147, 63, 136, 2, 145, 22, 186, 158, 46, 167, 198, 53, 158, 213, 151, 148, 166, 167, 223, 187, 242, 55, 119, 90, 26, 126, 220, 142, 29, 108, 17, 101, 113, 152, 150, 98, 168, 110, 202, 192, 240, 28, 163, 163, 7, 95, 73, 210, 20, 224, 133, 230, 164, 128, 156 }, new byte[] { 232, 136, 2, 31, 82, 230, 191, 255, 169, 210, 154, 244, 81, 189, 154, 228, 14, 63, 145, 20, 184, 96, 14, 72, 124, 204, 77, 32, 34, 78, 77, 174, 36, 88, 10, 51, 106, 107, 42, 29, 166, 177, 165, 28, 227, 86, 4, 52, 15, 179, 217, 25, 109, 197, 11, 2, 8, 221, 50, 39, 10, 224, 164, 153, 251, 66, 89, 4, 76, 120, 71, 83, 219, 65, 103, 24, 134, 229, 105, 232, 35, 19, 106, 10, 182, 196, 132, 43, 152, 65, 73, 213, 175, 123, 174, 43, 117, 74, 35, 178, 39, 25, 81, 37, 184, 168, 121, 181, 157, 29, 253, 231, 220, 209, 175, 142, 74, 25, 225, 200, 27, 190, 96, 44, 119, 183, 121, 194 } });
        }
    }
}
