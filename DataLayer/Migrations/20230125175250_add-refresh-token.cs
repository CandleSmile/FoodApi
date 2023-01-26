using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addrefreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { new byte[] { 74, 12, 236, 106, 16, 39, 237, 202, 101, 103, 155, 17, 154, 105, 202, 222, 40, 195, 252, 207, 21, 51, 97, 252, 219, 62, 77, 208, 72, 226, 184, 20, 106, 49, 168, 131, 182, 213, 71, 224, 204, 220, 70, 16, 56, 179, 96, 186, 210, 126, 159, 218, 76, 8, 175, 104, 143, 238, 229, 47, 236, 149, 49, 121 }, new byte[] { 238, 242, 120, 212, 79, 197, 92, 142, 63, 199, 176, 100, 159, 11, 146, 23, 231, 86, 153, 142, 90, 114, 67, 237, 122, 129, 223, 255, 69, 78, 142, 76, 171, 76, 35, 57, 108, 58, 249, 103, 100, 227, 190, 59, 134, 181, 141, 83, 196, 202, 222, 147, 216, 71, 246, 81, 65, 92, 210, 2, 130, 150, 185, 184, 248, 63, 63, 99, 208, 91, 0, 254, 200, 32, 229, 205, 44, 209, 221, 71, 168, 232, 16, 64, 220, 210, 7, 168, 231, 46, 146, 101, 149, 190, 210, 166, 7, 242, 161, 150, 62, 82, 49, 15, 34, 134, 126, 206, 27, 124, 248, 38, 17, 142, 13, 72, 97, 64, 238, 72, 218, 52, 236, 50, 167, 179, 39, 189 }, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 179, 146, 49, 35, 146, 251, 10, 178, 134, 194, 143, 146, 148, 236, 46, 61, 142, 204, 92, 99, 25, 89, 84, 90, 66, 67, 102, 126, 126, 64, 45, 234, 67, 53, 226, 95, 119, 157, 246, 35, 248, 236, 128, 232, 9, 246, 181, 147, 114, 154, 183, 196, 38, 176, 84, 67, 2, 87, 220, 7, 81, 206, 198, 78 }, new byte[] { 150, 34, 17, 76, 253, 87, 136, 31, 40, 114, 125, 32, 113, 104, 241, 41, 202, 207, 127, 9, 35, 205, 120, 198, 129, 44, 168, 14, 151, 44, 126, 31, 162, 160, 109, 20, 223, 237, 11, 67, 185, 253, 110, 245, 216, 128, 69, 80, 92, 169, 54, 156, 232, 243, 16, 98, 45, 32, 58, 192, 45, 34, 62, 69, 238, 172, 235, 196, 140, 113, 0, 24, 158, 236, 126, 233, 239, 241, 237, 189, 212, 147, 140, 21, 122, 118, 80, 190, 151, 245, 217, 217, 113, 117, 228, 208, 136, 116, 70, 58, 248, 145, 9, 188, 116, 53, 99, 126, 70, 214, 172, 170, 147, 128, 79, 241, 248, 100, 82, 89, 206, 88, 182, 254, 100, 252, 245, 138 } });
        }
    }
}
