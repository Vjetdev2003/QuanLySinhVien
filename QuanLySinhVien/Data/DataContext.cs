using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<SinhVien>SinhViens { get; set; }
        public DbSet<MonHoc> MonHoc { get; set; }
        public DbSet<Khoa> Khoa { get; set; }
        public DbSet<Lop> Lop { get; set; }
        public DbSet<DiemThi> DiemThi { get; set; }
        public DbSet<SinhVienLop>SinhVienLops { get; set; }
        public DbSet<KhoaLop>KhoaLops { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình SinhVien
            modelBuilder.Entity<SinhVien>()
                .HasKey(sv => sv.MaSV);

            modelBuilder.Entity<SinhVien>()
                .HasOne(sv => sv.Lop)
                .WithMany(l => l.SinhViens)
                .HasForeignKey(sv => sv.MaLop)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cascade paths

            // Cấu hình MonHoc
            modelBuilder.Entity<MonHoc>()
                .HasKey(mh => mh.MaMonHoc);

            // Cấu hình DiemThi
            modelBuilder.Entity<DiemThi>()
                .HasKey(dt => new { dt.MaMonHoc, dt.MaSV });

            modelBuilder.Entity<DiemThi>()
                .HasOne(dt => dt.MonHoc)
                .WithMany(mh => mh.DiemThi)
                .HasForeignKey(dt => dt.MaMonHoc)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cascade paths

            modelBuilder.Entity<DiemThi>()
                .HasOne(dt => dt.SinhVien)
                .WithMany(sv => sv.DiemThi)
                .HasForeignKey(dt => dt.MaSV)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cascade paths

            // Cấu hình Lop
            modelBuilder.Entity<Lop>()
                .HasKey(l => l.MaLop);

            modelBuilder.Entity<Lop>()
                .HasOne(l => l.Khoa)
                .WithMany(k => k.Lops)
                .HasForeignKey(l => l.MaKhoa)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cascade paths

            // Cấu hình Khoa
            modelBuilder.Entity<Khoa>()
                .HasKey(k => k.MaKhoa);

            // Mối quan hệ giữa Khoa và Lop thông qua KhoaLop
            modelBuilder.Entity<KhoaLop>()
                .HasKey(kl => new { kl.MaKhoa, kl.MaLop });

            modelBuilder.Entity<KhoaLop>()
                .HasOne(kl => kl.Khoa)
                .WithMany(k => k.KhoaLops)
                .HasForeignKey(kl => kl.MaKhoa)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cascade paths

            modelBuilder.Entity<KhoaLop>()
                .HasOne(kl => kl.Lop)
                .WithMany(l => l.KhoaLops)
                .HasForeignKey(kl => kl.MaLop)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cascade paths

            // Mối quan hệ giữa SinhVien và Lop thông qua SinhVienLop
            modelBuilder.Entity<SinhVienLop>()
                .HasKey(svl => new { svl.MaSV, svl.MaLop });

            modelBuilder.Entity<SinhVienLop>()
                .HasOne(svl => svl.SinhVien)
                .WithMany(sv => sv.SinhVienLops)
                .HasForeignKey(svl => svl.MaSV)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cascade paths

            modelBuilder.Entity<SinhVienLop>()
                .HasOne(svl => svl.Lop)
                .WithMany(l => l.SinhVienLops)
                .HasForeignKey(svl => svl.MaLop)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cascade paths
        }

    }
}
