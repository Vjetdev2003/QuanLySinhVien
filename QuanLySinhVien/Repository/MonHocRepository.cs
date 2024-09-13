using AutoMapper;
using QuanLySinhVien.Data;
using QuanLySinhVien.Interfaces;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Repository
{
    public class MonHocRepository : IMonHocRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MonHocRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateMonHoc(MonHoc monHoc)
        {
            _context.Add(monHoc);
            return Save();
        }

        public bool DeleteMonHoc(MonHoc monHoc)
        {
            _context.Remove(monHoc);
            return Save();
        }

        public MonHoc GetMonHoc(int maMonHoc)
        {
            return _context.MonHoc.Where(m => m.MaMonHoc == maMonHoc).SingleOrDefault();
        }

        public MonHoc GetMonHoc(string name)
        {
            return _context.MonHoc.Where(m => m.TenMonHoc == name).SingleOrDefault();
        }

        public ICollection<MonHoc> GetMonHocs()
        {
            return _context.MonHoc.ToList();
        }

        public bool MonHocExists(int maMonHoc)
        {
            return _context.MonHoc.Any(m=>m.MaMonHoc == maMonHoc);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMonHoc(MonHoc monHoc)
        {
            _context.Update(monHoc);
            return Save();
        }
    }
}
