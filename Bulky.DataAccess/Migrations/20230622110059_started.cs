using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class started : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Price50 = table.Column<float>(type: "real", nullable: false),
                    Price100 = table.Column<float>(type: "real", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 3, "Action" },
                    { 2, 2, "Sci-Fi" },
                    { 3, 5, "Drama" },
                    { 4, 1, "Crime" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AuthurName", "CategoryId", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Gideon", 2, "a book written by Oknko Chibodzor", "SNG234JUT34", 45f, 34f, 56f, 89f, "All that Glitters" },
                    { 2, "Gideon", 2, "a book written by Oknko Chibodzor", "HG985652LN", 45f, 34f, 56f, 89f, "Amen to all Prayers" },
                    { 3, "Gideon", 2, "There is a land that is farer than the day!", "JI87541256DS", 45f, 34f, 56f, 89f, "A charge to keep I have" },
                    { 4, "Gideon", 2, "With God, all things are possible", "BN568452SD", 45f, 34f, 56f, 89f, "Redeemer Lives" },
                    { 5, "Gideon", 2, "a book written by Oknko Chibodzor", "MN7585212SW", 45f, 34f, 56f, 89f, "All Oddss" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
