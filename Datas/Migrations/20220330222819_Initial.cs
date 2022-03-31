using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Datas.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "latin1");

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    no_hp = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    profil_picture = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "alamat",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kecamatan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    kelurahan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    rt = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    rw = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    kode_pos = table.Column<int>(type: "int(11)", nullable: false),
                    detail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alamat", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "kategori",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    deskripsi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    icon = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategori", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "produk",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    deskripsi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    harga = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    stock = table.Column<int>(type: "int(11)", nullable: false),
                    gambar = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produk", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "status_order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    deskripsi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_order", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    no_hp = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    profil_picture = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    is_admin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                    table.ForeignKey(
                        name: "customer_FK",
                        column: x => x.id_alamat,
                        principalTable: "alamat",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "kategori_produk",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_produk = table.Column<int>(type: "int(11)", nullable: false),
                    id_kategori = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategori_produk", x => x.id);
                    table.ForeignKey(
                        name: "kategori_produk_FK",
                        column: x => x.id_kategori,
                        principalTable: "kategori",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "kategori_produk_FK_1",
                        column: x => x.id_produk,
                        principalTable: "produk",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "keranjang",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_produk = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    jumlah_barang = table.Column<int>(type: "int(11)", nullable: false),
                    sub_total = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keranjang", x => x.id);
                    table.ForeignKey(
                        name: "keranjang_customer",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "keranjang_produk",
                        column: x => x.id_produk,
                        principalTable: "produk",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_keranjang = table.Column<int>(type: "int(11)", nullable: false),
                    tanggal_transaksi = table.Column<DateOnly>(type: "date", nullable: false),
                    jumlahbayar = table.Column<decimal>(name: "jumlah bayar", type: "decimal(10)", precision: 10, nullable: true),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    id_status = table.Column<int>(type: "int(11)", nullable: false),
                    catatan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "order_alamat",
                        column: x => x.id_alamat,
                        principalTable: "alamat",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "order_customer",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "order_keranjang",
                        column: x => x.id_keranjang,
                        principalTable: "keranjang",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "order_status",
                        column: x => x.id_status,
                        principalTable: "status_order",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "pembayaran",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    metode_pembayaran = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    total_bayar = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    tanggal = table.Column<DateOnly>(type: "date", nullable: false),
                    tujuan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    pajak = table.Column<int>(type: "int(11)", nullable: true),
                    status = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pembayaran", x => x.id);
                    table.ForeignKey(
                        name: "pembayaran_customer",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "pembayaran_order",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "pengiriman",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    kurir = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    ongkir = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false),
                    status = table.Column<int>(type: "int(11)", nullable: false),
                    keterangan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pengiriman", x => x.id);
                    table.ForeignKey(
                        name: "pengiriman_FK",
                        column: x => x.id_alamat,
                        principalTable: "alamat",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "pengiriman_order",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateIndex(
                name: "customer_FK",
                table: "customer",
                column: "id_alamat");

            migrationBuilder.CreateIndex(
                name: "kategori_produk_FK",
                table: "kategori_produk",
                column: "id_kategori");

            migrationBuilder.CreateIndex(
                name: "kategori_produk_FK_1",
                table: "kategori_produk",
                column: "id_produk");

            migrationBuilder.CreateIndex(
                name: "keranjang_customer",
                table: "keranjang",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "keranjang_produk",
                table: "keranjang",
                column: "id_produk");

            migrationBuilder.CreateIndex(
                name: "order_alamat",
                table: "order",
                column: "id_alamat");

            migrationBuilder.CreateIndex(
                name: "order_customer",
                table: "order",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "order_FK",
                table: "order",
                column: "id_keranjang");

            migrationBuilder.CreateIndex(
                name: "order_status",
                table: "order",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "pembayaran_customer",
                table: "pembayaran",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "pembayaran_order",
                table: "pembayaran",
                column: "id_order");

            migrationBuilder.CreateIndex(
                name: "pengiriman_FK",
                table: "pengiriman",
                column: "id_alamat");

            migrationBuilder.CreateIndex(
                name: "pengiriman_order",
                table: "pengiriman",
                column: "id_order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "kategori_produk");

            migrationBuilder.DropTable(
                name: "pembayaran");

            migrationBuilder.DropTable(
                name: "pengiriman");

            migrationBuilder.DropTable(
                name: "kategori");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "keranjang");

            migrationBuilder.DropTable(
                name: "status_order");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "produk");

            migrationBuilder.DropTable(
                name: "alamat");
        }
    }
}
