using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BooksApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    PageCount = table.Column<int>(type: "INTEGER", nullable: false),
                    EditionYear = table.Column<int>(type: "INTEGER", nullable: false),
                    EditionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => new { x.BookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BookCategories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "CreatedDate", "Gender", "IsApproved", "ModifiedDate", "Name", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6764), "E", true, new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6785), "Orhan Parasaçan", "orhan-parasacan" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6791), "E", true, new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6793), "Selami Gülgeçen", "selami-gulgecen" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6797), "E", true, new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6799), "Seyhan Yolagelen", "seyhan-yolagelen" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6804), "E", true, new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6805), "Hale Çokseven", "hale-cokseven" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6808), "E", true, new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6809), "Kemal Devabulan", "kemal-devabulan" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6812), "E", true, new DateTime(2023, 3, 24, 17, 28, 36, 285, DateTimeKind.Local).AddTicks(6814), "Selen Günebakan", "selen-gunebakan" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedDate", "EditionNumber", "EditionYear", "IsApproved", "ModifiedDate", "Name", "PageCount", "Price", "Stock", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1330), 1, 2020, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1343), "Fahrenheit 451", 150, 75m, 10, "fahrenheit-451" },
                    { 2, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1354), 43, 2010, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1355), "Kadınlar Ülkesi", 120, 175m, 10, "kadinlar-ulkesi" },
                    { 3, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1360), 13, 2010, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1362), "İnsanlar", 180, 705m, 10, "insanlar" },
                    { 4, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1368), 2, 2022, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1369), "Görünmez Adam", 190, 15m, 10, "gorunmez-adam" },
                    { 5, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1374), 13, 2018, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1376), "Siyah Günce", 250, 47m, 10, "siyah-gunce" },
                    { 6, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1382), 8, 2016, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1384), "Nebo'nun Mavi Kitabı", 50, 68m, 10, "nebonun-mavi-kitabi" },
                    { 7, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1388), 7, 2021, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1390), "Evrenin Sonundaki Restoran", 15, 114m, 0, "evrenin-sonundaki-restoran" },
                    { 8, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1394), 4, 2020, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1396), "Beni Kim Öldürdü?", 330, 247m, 10, "beni-kim-oldurdu" },
                    { 9, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1401), 1, 2022, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1402), "Zihin Kütüphanesi", 240, 300m, 0, "zihin-kutuphanesi" },
                    { 10, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1407), 11, 2019, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1408), "Yeni Bir Yaşam", 400, 166m, 10, "yeni-bir-yasam" },
                    { 11, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1413), 12, 2020, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1414), "Gecenin Kraliçesi", 97, 19m, 10, "gecenin-kralicesi" },
                    { 12, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1419), 10, 2020, true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(1420), "Efendi Uyanıyor", 125, 75m, 10, "efendi-uyaniyor" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "IsApproved", "ModifiedDate", "Name", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(9591), "Edebiyat türü burada", true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(9605), "Edebiyat", "edebiyat" },
                    { 2, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(9609), "Başvuru kitapları burada", true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(9610), "Başvuru", "basvuru" },
                    { 3, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(9613), "Çocuk kitapları burada", true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(9614), "Çocuk", "cocuk" },
                    { 4, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(9617), "Ders kitapları burada", true, new DateTime(2023, 3, 24, 17, 28, 36, 289, DateTimeKind.Local).AddTicks(9618), "Ders Kitabı", "ders-kitabi" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 2, 7 },
                    { 1, 8 },
                    { 3, 9 },
                    { 4, 10 },
                    { 5, 11 },
                    { 6, 12 }
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "BookId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 2 },
                    { 8, 4 },
                    { 9, 2 },
                    { 9, 4 },
                    { 10, 2 },
                    { 10, 4 },
                    { 11, 2 },
                    { 11, 4 },
                    { 12, 3 },
                    { 12, 4 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "BookId", "CreatedDate", "IsApproved", "ModifiedDate", "Url" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6588), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6602), "1.jpg" },
                    { 2, 2, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6607), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6609), "2.jpg" },
                    { 3, 3, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6611), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6612), "3.jpg" },
                    { 4, 4, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6615), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6616), "4.jpg" },
                    { 5, 5, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6619), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6620), "5.jpg" },
                    { 6, 6, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6622), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6624), "6.jpg" },
                    { 7, 7, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6626), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6627), "7.jpg" },
                    { 8, 8, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6629), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6631), "8.jpg" },
                    { 9, 9, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6633), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6634), "9.jpg" },
                    { 10, 10, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6637), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6638), "10.jpg" },
                    { 11, 11, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6640), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6641), "11.jpg" },
                    { 12, 12, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6644), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6645), "12.jpg" },
                    { 13, 1, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6647), true, new DateTime(2023, 3, 24, 17, 28, 36, 290, DateTimeKind.Local).AddTicks(6648), "222.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_CategoryId",
                table: "BookCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BookId",
                table: "Images",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
