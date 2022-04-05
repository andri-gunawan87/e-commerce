using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Datas.Migrations
{
    public partial class altertableorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_keranjang_IdKeranjangNavigationId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_IdKeranjangNavigationId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "IdKeranjangNavigationId",
                table: "order");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "tanggal_transaksi",
                table: "order",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

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

            migrationBuilder.AddColumn<int>(
                name: "KeranjangId",
                table: "order",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "keranjang",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "subtotal",
                table: "detail_order",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "harga",
                table: "detail_order",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.CreateIndex(
                name: "IX_order_KeranjangId",
                table: "order",
                column: "KeranjangId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_keranjang_KeranjangId",
                table: "order",
                column: "KeranjangId",
                principalTable: "keranjang",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_keranjang_KeranjangId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_KeranjangId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "KeranjangId",
                table: "order");

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

            migrationBuilder.AlterColumn<DateOnly>(
                name: "tanggal_transaksi",
                table: "order",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

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

            migrationBuilder.AddColumn<int>(
                name: "IdKeranjangNavigationId",
                table: "order",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "keranjang",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "subtotal",
                table: "detail_order",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "harga",
                table: "detail_order",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.CreateIndex(
                name: "IX_order_IdKeranjangNavigationId",
                table: "order",
                column: "IdKeranjangNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_keranjang_IdKeranjangNavigationId",
                table: "order",
                column: "IdKeranjangNavigationId",
                principalTable: "keranjang",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
