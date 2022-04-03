using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Datas.Migrations
{
    public partial class editkolomalamatpadadatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "alamat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountRegisterViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(type: "longtext", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    NoHp = table.Column<string>(type: "longtext", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Username = table.Column<string>(type: "longtext", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Password = table.Column<string>(type: "longtext", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    ProfilPicture = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Email = table.Column<string>(type: "longtext", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdAlamat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRegisterViewModel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "KeranjangViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProduk = table.Column<int>(type: "int", nullable: false),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    JumlahBarang = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Gambar = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    NamaProduk = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeranjangViewModel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRegisterViewModel");

            migrationBuilder.DropTable(
                name: "KeranjangViewModel");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "alamat");

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
        }
    }
}
