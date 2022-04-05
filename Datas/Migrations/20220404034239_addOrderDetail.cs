using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Datas.Migrations
{
    public partial class addOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "order_keranjang",
                table: "order");

            migrationBuilder.RenameColumn(
                name: "id_keranjang",
                table: "order",
                newName: "IdKeranjangNavigationId");

            migrationBuilder.RenameIndex(
                name: "order_FK",
                table: "order",
                newName: "IX_order_IdKeranjangNavigationId");

            migrationBuilder.AlterColumn<decimal>(
                name: "harga",
                table: "produk",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "ongkir",
                table: "pengiriman",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_bayar",
                table: "pembayaran",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "jumlah bayar",
                table: "order",
                type: "decimal(10)",
                precision: 10,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "keranjang",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.CreateTable(
                name: "detail_order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    id_produk = table.Column<int>(type: "int", nullable: false),
                    harga = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    jml_barang = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detail_order", x => x.id);
                    table.ForeignKey(
                        name: "detail_order_FK_1",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateIndex(
                name: "detail_order_FK_1",
                table: "detail_order",
                column: "id_order");

            migrationBuilder.AddForeignKey(
                name: "FK_order_keranjang_IdKeranjangNavigationId",
                table: "order",
                column: "IdKeranjangNavigationId",
                principalTable: "keranjang",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_keranjang_IdKeranjangNavigationId",
                table: "order");

            migrationBuilder.DropTable(
                name: "detail_order");

            migrationBuilder.RenameColumn(
                name: "IdKeranjangNavigationId",
                table: "order",
                newName: "id_keranjang");

            migrationBuilder.RenameIndex(
                name: "IX_order_IdKeranjangNavigationId",
                table: "order",
                newName: "order_FK");

            migrationBuilder.AlterColumn<decimal>(
                name: "harga",
                table: "produk",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "ongkir",
                table: "pengiriman",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_bayar",
                table: "pembayaran",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "jumlah bayar",
                table: "order",
                type: "decimal(10,30)",
                precision: 10,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "keranjang",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AddForeignKey(
                name: "order_keranjang",
                table: "order",
                column: "id_keranjang",
                principalTable: "keranjang",
                principalColumn: "id");
        }
    }
}
