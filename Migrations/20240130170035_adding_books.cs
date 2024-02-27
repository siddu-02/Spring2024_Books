using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spring2024_Books.Migrations
{
    public partial class adding_books : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookTitle", "CategoryID", "Description", "Price" },
                values: new object[] { 1, "David Grann", "The Wager", 1, "Shipreck Turns to Murder", 19.99m });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookTitle", "CategoryID", "Description", "Price" },
                values: new object[] { 2, "Dr.Susse", "Cat in the Hat", 2, "What is in the Cat's Hat", 10.99m });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookTitle", "CategoryID", "Description", "Price" },
                values: new object[] { 3, "Laura Numeroff", "If You Give a Mouse a Cookie", 3, "About an Inspiring Mouse", 15.99m });

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryID",
                table: "Book",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");
        }
    }
}
