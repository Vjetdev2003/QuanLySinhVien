using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            // Seed Khoa data
            if (!_context.Khoa.Any())
            {
                var khoa = new List<Khoa>
                {
                    new Khoa { MaKhoa = 1, TenKhoa = "Khoa Công Nghệ Thông Tin", DienThoai = "0123456789" },
                    new Khoa { MaKhoa = 2, TenKhoa = "Khoa Kinh Tế", DienThoai = "0987654321" }
                };
                _context.Khoa.AddRange(khoa);
                _context.SaveChanges();
            }

            // Seed Lop data
            if (!_context.Lop.Any())
            {
                var lop = new List<Lop>
                {
                    new Lop { MaLop = 1, TenLop = "Lớp CNTT1", HeDaoTao = "Chính Quy", NamNhapHoc = 2023, SiSo = 40, MaKhoa = 1 },
                    new Lop { MaLop = 2, TenLop = "Lớp Kinh Tế1", HeDaoTao = "Chính Quy", NamNhapHoc = 2023, SiSo = 35, MaKhoa = 2 }
                };
                _context.Lop.AddRange(lop);
                _context.SaveChanges();
            }

            // Seed MonHoc data
            if (!_context.MonHoc.Any())
            {
                var monHoc = new List<MonHoc>
                {
                    new MonHoc { MaMonHoc = 1, TenMonHoc = "Lập Trình C#", SoDVHT = 3 },
                    new MonHoc { MaMonHoc = 2, TenMonHoc = "Kinh Tế Vi Mô", SoDVHT = 3 }
                };
                _context.MonHoc.AddRange(monHoc);
                _context.SaveChanges();
            }

            // Seed SinhVien data
            if (!_context.SinhViens.Any())
            {
                var sinhVien = new List<SinhVien>
                {
                    new SinhVien { MaSV = 1, HoDem = "Nguyễn Văn", Ten = "A", NgaySinh = new DateTime(2000, 1, 1), GioiTinh = true, NoiSinh = "Hà Nội", MaLop = 1 },
                    new SinhVien { MaSV = 2, HoDem = "Trần Thị", Ten = "B", NgaySinh = new DateTime(2000, 2, 1), GioiTinh = false, NoiSinh = "Hà Nội", MaLop = 1 }
                };
                _context.SinhViens.AddRange(sinhVien);
                _context.SaveChanges();
            }

            // Seed DiemThi data
            if (!_context.DiemThi.Any())
            {
                var diemThi = new List<DiemThi>
                {
                    new DiemThi { MaMonHoc = 1, MaSV = 1, DiemLan1 = 8.5, DiemLan2 = 9.0 },
                    new DiemThi { MaMonHoc = 2, MaSV = 2, DiemLan1 = 7.0, DiemLan2 = 8.0 }
                };
                _context.DiemThi.AddRange(diemThi);
                _context.SaveChanges();
            }
        }
    }
}
