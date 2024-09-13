using AutoMapper;
using QuanLySinhVien.Data;
using QuanLySinhVien.Interfaces;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Repository
{
    public class DiemThiRepository : IDiemThiRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DiemThiRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateDiemThi(DiemThi diemThi)
        {
            _context.Add(diemThi);
            return Save();
        }

        public bool DeleteDiemThi(int maMonHoc, int maSV)
        {
            var diemThi = GetDiemThi(maMonHoc, maSV);
            if (diemThi == null)
            {
                return false;
            }
            _context.Remove(diemThi);
            return Save();
        }

        public bool DiemThiExists(int maMonHoc, int maSV)
        {
            return _context.DiemThi.Any(d=>d.MaMonHoc == maMonHoc && d.MaSV == maSV);
        }

        public DiemThi GetDiemThi(int maMonHoc, int maSV)
        {
            return _context.DiemThi
                .FirstOrDefault(dt => dt.MaMonHoc == maMonHoc && dt.MaSV == maSV);
        }

        public IEnumerable<DiemThi> GetDiemThis()
        {
           return _context.DiemThi.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDiemThi(DiemThi diemThi)
        {
            _context.Update(diemThi);
            return Save();
        }
    }
}
