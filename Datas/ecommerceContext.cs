using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using e_commerce.Datas.Entities;
using e_commerce.ViewModels;

namespace e_commerce.Datas
{
    public partial class ecommerceContext : DbContext
    {
        public ecommerceContext()
        {
        }

        public ecommerceContext(DbContextOptions<ecommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Alamat> Alamats { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Kategori> Kategoris { get; set; } = null!;
        public virtual DbSet<KategoriProduk> KategoriProduks { get; set; } = null!;
        public virtual DbSet<Keranjang> Keranjangs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Pembayaran> Pembayarans { get; set; } = null!;
        public virtual DbSet<Pengiriman> Pengirimen { get; set; } = null!;
        public virtual DbSet<Produk> Produks { get; set; } = null!;
        public virtual DbSet<StatusOrder> StatusOrders { get; set; } = null!;
        public virtual DbSet<DetailOrder> DetailOrders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Nama)
                    .HasMaxLength(100)
                    .HasColumnName("nama");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(100)
                    .HasColumnName("no_hp");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.ProfilPicture)
                    .HasMaxLength(100)
                    .HasColumnName("profil_picture");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Alamat>(entity =>
            {
                entity.ToTable("alamat");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Detail)
                    .HasMaxLength(100)
                    .HasColumnName("detail");

                entity.Property(e => e.Kecamatan)
                    .HasMaxLength(100)
                    .HasColumnName("kecamatan");

                entity.Property(e => e.Kelurahan)
                    .HasMaxLength(100)
                    .HasColumnName("kelurahan");

                entity.Property(e => e.KodePos)
                    .HasColumnType("int(11)")
                    .HasColumnName("kode_pos");

                entity.Property(e => e.Rt)
                    .HasMaxLength(100)
                    .HasColumnName("rt");

                entity.Property(e => e.Rw)
                    .HasMaxLength(100)
                    .HasColumnName("rw");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.IdAlamat, "customer_FK");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Nama)
                    .HasMaxLength(100)
                    .HasColumnName("nama");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(100)
                    .HasColumnName("no_hp");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.ProfilPicture)
                    .HasMaxLength(100)
                    .HasColumnName("profil_picture");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasOne(d => d.IdAlamatNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.IdAlamat)
                    .HasConstraintName("customer_FK");
            });

            modelBuilder.Entity<DetailOrder>(entity => {

                entity.ToTable("detail_order");

                entity.HasIndex(e => e.IdOrder, "detail_order_FK_1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.IdProduk).HasColumnName("id_produk");

                entity.Property(e => e.Harga).HasColumnName("harga")
                .HasPrecision(10);

                entity.Property(e => e.JumlahBarang).HasColumnName("jml_barang");

                entity.Property(e => e.SubTotal).HasColumnName("subtotal")
                .HasPrecision(10);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.DetailOrder)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detail_order_FK_1");
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.ToTable("kategori");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(100)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Icon)
                    .HasMaxLength(100)
                    .HasColumnName("icon");

                entity.Property(e => e.Nama)
                    .HasMaxLength(100)
                    .HasColumnName("nama");
            });

            modelBuilder.Entity<KategoriProduk>(entity =>
            {
                entity.ToTable("kategori_produk");

                entity.HasIndex(e => e.IdKategori, "kategori_produk_FK");

                entity.HasIndex(e => e.IdProduk, "kategori_produk_FK_1");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_kategori");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.HasOne(d => d.IdKategoriNavigation)
                    .WithMany(p => p.KategoriProduks)
                    .HasForeignKey(d => d.IdKategori)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("kategori_produk_FK");

                entity.HasOne(d => d.IdProdukNavigation)
                    .WithMany(p => p.KategoriProduks)
                    .HasForeignKey(d => d.IdProduk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("kategori_produk_FK_1");
            });

            modelBuilder.Entity<Keranjang>(entity =>
            {
                entity.ToTable("keranjang");

                entity.HasIndex(e => e.IdCustomer, "keranjang_customer");

                entity.HasIndex(e => e.IdProduk, "keranjang_produk");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.Property(e => e.JumlahBarang)
                    .HasColumnType("int(11)")
                    .HasColumnName("jumlah_barang");

                entity.Property(e => e.SubTotal)
                    .HasPrecision(10)
                    .HasColumnName("sub_total");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("keranjang_customer");

                entity.HasOne(d => d.IdProdukNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdProduk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("keranjang_produk");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.IdAlamat, "order_alamat");

                entity.HasIndex(e => e.IdCustomer, "order_customer");

                entity.HasIndex(e => e.IdStatus, "order_status");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Catatan)
                    .HasMaxLength(100)
                    .HasColumnName("catatan");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.IdStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_status");

                entity.Property(e => e.JumlahBayar)
                    .HasPrecision(10)
                    .HasColumnName("jumlah bayar");

                entity.Property(e => e.TanggalTransaksi).HasColumnName("tanggal_transaksi");

                entity.HasOne(d => d.IdAlamatNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdAlamat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_alamat");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_customer");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_status");
            });

            modelBuilder.Entity<Pembayaran>(entity =>
            {
                entity.ToTable("pembayaran");

                entity.HasIndex(e => e.IdCustomer, "pembayaran_customer");

                entity.HasIndex(e => e.IdOrder, "pembayaran_order");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.MetodePembayaran)
                    .HasMaxLength(100)
                    .HasColumnName("metode_pembayaran");

                entity.Property(e => e.Pajak)
                    .HasColumnType("int(11)")
                    .HasColumnName("pajak");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Tanggal).HasColumnName("tanggal");

                entity.Property(e => e.TotalBayar)
                    .HasPrecision(10)
                    .HasColumnName("total_bayar");

                entity.Property(e => e.Tujuan)
                    .HasMaxLength(100)
                    .HasColumnName("tujuan");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pembayaran_customer");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pembayaran_order");
            });

            modelBuilder.Entity<Pengiriman>(entity =>
            {
                entity.ToTable("pengiriman");

                entity.HasIndex(e => e.IdAlamat, "pengiriman_FK");

                entity.HasIndex(e => e.IdOrder, "pengiriman_order");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.Keterangan)
                    .HasMaxLength(100)
                    .HasColumnName("keterangan");

                entity.Property(e => e.Kurir)
                    .HasMaxLength(100)
                    .HasColumnName("kurir");

                entity.Property(e => e.Ongkir)
                    .HasPrecision(10)
                    .HasColumnName("ongkir");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status");

                entity.Property(e => e.NoResi)
                    .HasMaxLength(100)
                    .HasColumnName("no_resi");

                entity.HasOne(d => d.IdAlamatNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.IdAlamat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pengiriman_FK");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pengiriman_order");
            });

            modelBuilder.Entity<Produk>(entity =>
            {
                entity.ToTable("produk");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(100)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Gambar)
                    .HasMaxLength(100)
                    .HasColumnName("gambar");

                entity.Property(e => e.Harga)
                    .HasPrecision(10)
                    .HasColumnName("harga");

                entity.Property(e => e.Nama)
                    .HasMaxLength(100)
                    .HasColumnName("nama");

                entity.Property(e => e.Stock)
                    .HasColumnType("int(11)")
                    .HasColumnName("stock");
            });

            modelBuilder.Entity<StatusOrder>(entity =>
            {
                entity.ToTable("status_order");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(100)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Nama)
                    .HasMaxLength(100)
                    .HasColumnName("nama");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
