using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MarketSitesi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yiyecekler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StokAdedi = table.Column<int>(type: "int", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yiyecekler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yiyecekler_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Icecekler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StokAdedi = table.Column<int>(type: "int", nullable: false),
                    Hacim = table.Column<int>(type: "int", nullable: false),
                    YiyecekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icecekler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Icecekler_Yiyecekler_YiyecekId",
                        column: x => x.YiyecekId,
                        principalTable: "Yiyecekler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kategoriler",
                columns: new[] { "Id", "Aciklama", "Ad" },
                values: new object[,]
                {
                    { 1, "Et ve tavuk ürünleri", "Et ve Tavuk" },
                    { 2, "Süt ve süt ürünleri", "Süt Ürünleri" },
                    { 3, "Taze meyve ve sebzeler", "Meyve ve Sebze" },
                    { 4, "Çeşitli içecekler", "İçecekler" }
                });

            migrationBuilder.InsertData(
                table: "Yiyecekler",
                columns: new[] { "Id", "Aciklama", "Ad", "Fiyat", "KategoriId", "StokAdedi" },
                values: new object[,]
                {
                    { 1, "Taze tavuk göğsü", "Tavuk Göğsü", 45.50m, 1, 20 },
                    { 2, "1 litre tam yağlı süt", "Süt", 12.75m, 2, 50 },
                    { 3, "Kırmızı elma", "Elma", 8.90m, 3, 100 }
                });

            migrationBuilder.InsertData(
                table: "Icecekler",
                columns: new[] { "Id", "Aciklama", "Ad", "Fiyat", "Hacim", "StokAdedi", "YiyecekId" },
                values: new object[,]
                {
                    { 1, "Gazlı içecek", "Kola", 5.50m, 330, 200, 1 },
                    { 2, "Doğal kaynak suyu", "Su", 2.00m, 500, 500, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Icecekler_YiyecekId",
                table: "Icecekler",
                column: "YiyecekId");

            migrationBuilder.CreateIndex(
                name: "IX_Yiyecekler_KategoriId",
                table: "Yiyecekler",
                column: "KategoriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Icecekler");

            migrationBuilder.DropTable(
                name: "Yiyecekler");

            migrationBuilder.DropTable(
                name: "Kategoriler");
        }
    }
}
