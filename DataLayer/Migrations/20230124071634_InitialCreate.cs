using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, new byte[] { 179, 146, 49, 35, 146, 251, 10, 178, 134, 194, 143, 146, 148, 236, 46, 61, 142, 204, 92, 99, 25, 89, 84, 90, 66, 67, 102, 126, 126, 64, 45, 234, 67, 53, 226, 95, 119, 157, 246, 35, 248, 236, 128, 232, 9, 246, 181, 147, 114, 154, 183, 196, 38, 176, 84, 67, 2, 87, 220, 7, 81, 206, 198, 78 }, new byte[] { 150, 34, 17, 76, 253, 87, 136, 31, 40, 114, 125, 32, 113, 104, 241, 41, 202, 207, 127, 9, 35, 205, 120, 198, 129, 44, 168, 14, 151, 44, 126, 31, 162, 160, 109, 20, 223, 237, 11, 67, 185, 253, 110, 245, 216, 128, 69, 80, 92, 169, 54, 156, 232, 243, 16, 98, 45, 32, 58, 192, 45, 34, 62, 69, 238, 172, 235, 196, 140, 113, 0, 24, 158, 236, 126, 233, 239, 241, 237, 189, 212, 147, 140, 21, 122, 118, 80, 190, 151, 245, 217, 217, 113, 117, 228, 208, 136, 116, 70, 58, 248, 145, 9, 188, 116, 53, 99, 126, 70, 214, 172, 170, 147, 128, 79, 241, 248, 100, 82, 89, 206, 88, 182, 254, 100, 252, 245, 138 }, "lizaveta.razumovich@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
