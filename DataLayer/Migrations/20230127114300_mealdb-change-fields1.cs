using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class mealdbchangefields1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderItems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Meals",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Areas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 214, 87, 236, 96, 205, 224, 71, 127, 91, 147, 63, 136, 2, 145, 22, 186, 158, 46, 167, 198, 53, 158, 213, 151, 148, 166, 167, 223, 187, 242, 55, 119, 90, 26, 126, 220, 142, 29, 108, 17, 101, 113, 152, 150, 98, 168, 110, 202, 192, 240, 28, 163, 163, 7, 95, 73, 210, 20, 224, 133, 230, 164, 128, 156 }, new byte[] { 232, 136, 2, 31, 82, 230, 191, 255, 169, 210, 154, 244, 81, 189, 154, 228, 14, 63, 145, 20, 184, 96, 14, 72, 124, 204, 77, 32, 34, 78, 77, 174, 36, 88, 10, 51, 106, 107, 42, 29, 166, 177, 165, 28, 227, 86, 4, 52, 15, 179, 217, 25, 109, 197, 11, 2, 8, 221, 50, 39, 10, 224, 164, 153, 251, 66, 89, 4, 76, 120, 71, 83, 219, 65, 103, 24, 134, 229, 105, 232, 35, 19, 106, 10, 182, 196, 132, 43, 152, 65, 73, 213, 175, 123, 174, 43, 117, 74, 35, 178, 39, 25, 81, 37, 184, 168, 121, 181, 157, 29, 253, 231, 220, 209, 175, 142, 74, 25, 225, 200, 27, 190, 96, 44, 119, 183, 121, 194 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 144, 73, 251, 235, 196, 217, 41, 195, 159, 190, 193, 39, 1, 52, 120, 145, 59, 200, 13, 182, 252, 231, 38, 214, 190, 219, 251, 11, 120, 56, 117, 32, 44, 162, 236, 37, 56, 119, 140, 81, 111, 175, 118, 167, 159, 130, 35, 92, 102, 175, 144, 147, 127, 18, 83, 228, 62, 144, 240, 104, 231, 75, 251, 86 }, new byte[] { 13, 128, 94, 112, 132, 36, 47, 123, 120, 224, 131, 40, 78, 52, 15, 168, 17, 243, 119, 6, 68, 195, 185, 30, 162, 77, 252, 91, 252, 205, 199, 147, 52, 182, 234, 154, 187, 204, 143, 231, 245, 100, 140, 107, 178, 38, 64, 178, 12, 213, 4, 222, 66, 28, 172, 194, 239, 15, 99, 123, 47, 170, 57, 161, 30, 86, 2, 64, 34, 85, 234, 143, 36, 50, 79, 218, 116, 37, 141, 245, 35, 34, 6, 136, 181, 14, 119, 253, 43, 51, 105, 95, 127, 221, 219, 31, 239, 118, 239, 20, 157, 75, 236, 17, 186, 29, 246, 93, 85, 153, 162, 80, 11, 160, 46, 192, 200, 14, 129, 4, 15, 6, 124, 67, 229, 225, 37, 250 } });
        }
    }
}
