using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class mealdbcreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealIngredients",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    MealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealIngredients", x => new { x.IngredientsId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_MealIngredients_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealIngredients_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealTags",
                columns: table => new
                {
                    MealsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTags", x => new { x.MealsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_MealTags_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 144, 73, 251, 235, 196, 217, 41, 195, 159, 190, 193, 39, 1, 52, 120, 145, 59, 200, 13, 182, 252, 231, 38, 214, 190, 219, 251, 11, 120, 56, 117, 32, 44, 162, 236, 37, 56, 119, 140, 81, 111, 175, 118, 167, 159, 130, 35, 92, 102, 175, 144, 147, 127, 18, 83, 228, 62, 144, 240, 104, 231, 75, 251, 86 }, new byte[] { 13, 128, 94, 112, 132, 36, 47, 123, 120, 224, 131, 40, 78, 52, 15, 168, 17, 243, 119, 6, 68, 195, 185, 30, 162, 77, 252, 91, 252, 205, 199, 147, 52, 182, 234, 154, 187, 204, 143, 231, 245, 100, 140, 107, 178, 38, 64, 178, 12, 213, 4, 222, 66, 28, 172, 194, 239, 15, 99, 123, 47, 170, 57, 161, 30, 86, 2, 64, 34, 85, 234, 143, 36, 50, 79, 218, 116, 37, 141, 245, 35, 34, 6, 136, 181, 14, 119, 253, 43, 51, 105, 95, 127, 221, 219, 31, 239, 118, 239, 20, 157, 75, 236, 17, 186, 29, 246, 93, 85, 153, 162, 80, 11, 160, 46, 192, 200, 14, 129, 4, 15, 6, 124, 67, 229, 225, 37, 250 } });

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredients_MealsId",
                table: "MealIngredients",
                column: "MealsId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_AreaId",
                table: "Meals",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_CategoryId",
                table: "Meals",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTags_TagsId",
                table: "MealTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MealId",
                table: "OrderItems",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealIngredients");

            migrationBuilder.DropTable(
                name: "MealTags");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 74, 12, 236, 106, 16, 39, 237, 202, 101, 103, 155, 17, 154, 105, 202, 222, 40, 195, 252, 207, 21, 51, 97, 252, 219, 62, 77, 208, 72, 226, 184, 20, 106, 49, 168, 131, 182, 213, 71, 224, 204, 220, 70, 16, 56, 179, 96, 186, 210, 126, 159, 218, 76, 8, 175, 104, 143, 238, 229, 47, 236, 149, 49, 121 }, new byte[] { 238, 242, 120, 212, 79, 197, 92, 142, 63, 199, 176, 100, 159, 11, 146, 23, 231, 86, 153, 142, 90, 114, 67, 237, 122, 129, 223, 255, 69, 78, 142, 76, 171, 76, 35, 57, 108, 58, 249, 103, 100, 227, 190, 59, 134, 181, 141, 83, 196, 202, 222, 147, 216, 71, 246, 81, 65, 92, 210, 2, 130, 150, 185, 184, 248, 63, 63, 99, 208, 91, 0, 254, 200, 32, 229, 205, 44, 209, 221, 71, 168, 232, 16, 64, 220, 210, 7, 168, 231, 46, 146, 101, 149, 190, 210, 166, 7, 242, 161, 150, 62, 82, 49, 15, 34, 134, 126, 206, 27, 124, 248, 38, 17, 142, 13, 72, 97, 64, 238, 72, 218, 52, 236, 50, 167, 179, 39, 189 } });
        }
    }
}
