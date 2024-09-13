using AutoMapper;
using QuanLySinhVien.Data;
using QuanLySinhVien.Interfaces;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Repository
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SinhVienRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateSinhVien(SinhVien sv)
        {
            _context.Add(sv);
            return Save();
        }

        public bool DeleteSinhVien(int maSV)
        {
            _context.Remove(maSV);
            return Save();
        }

        public SinhVien GetSinhVien(int id)
        {
            return _context.SinhViens.Where(s => s.MaSV == id).FirstOrDefault();
        }

        public SinhVien GetSinhVien(string name)
        {
            return _context.SinhViens.Where(s => s.Ten == name).FirstOrDefault();
        }

        public IEnumerable<SinhVien> GetSinhVienByLop(int maLop)
        {
            return _context.SinhViens.Where(l=>l.MaLop == maLop).ToList();
        }

        public ICollection<SinhVien> GetSinhViens()
        {
            return _context.SinhViens.ToList();
        }

        public bool Save()
        {
            var saved =_context.SaveChanges();
            return saved > 0 ? true : false;

        }

        public bool SinhVienExists(int sinhvienId)
        {
            return _context.SinhViens.Any(s => s.MaSV == sinhvienId);
        }

        public bool UpdateSinhVien(SinhVien sinhVien)
        {
            var existingSinhVien = _context.SinhViens.FirstOrDefault(sv => sv.MaSV == sinhVien.MaSV);
            if (existingSinhVien == null)
                return false;

            // Cập nhật thuộc tính
            existingSinhVien.HoDem = sinhVien.HoDem;
            existingSinhVien.Ten = sinhVien.Ten;
            existingSinhVien.NgaySinh = sinhVien.NgaySinh;
            existingSinhVien.GioiTinh = sinhVien.GioiTinh;
            existingSinhVien.NoiSinh = sinhVien.NoiSinh;
            existingSinhVien.MaLop = sinhVien.MaLop;

            // Save changes
            _context.SinhViens.Update(existingSinhVien);
            return _context.SaveChanges() > 0;
        }
    }
}
