using AutoMapper;
using QuanLySinhVien.Data;
using QuanLySinhVien.Interfaces;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Repository
{
    public class LopRepository : ILopRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LopRepository(DataContext context,IMapper mapper)
        {
            _context = context;
             _mapper = mapper;
        }
        public bool CreateLop(Lop lop)
        {
            _context.Add(lop);
            return Save();
        }

        public bool DeleteLop(int maLop)
        {
            _context.Remove(maLop);
            return Save();
        }

        public Lop GetLop(int maLop)
        {
            return _context.Lop.Where(l => l.MaLop == maLop).FirstOrDefault();

        }

        public Lop GetLop(string name)
        {
            return _context.Lop.Where(l => l.TenLop == name).FirstOrDefault();
        }

        public IEnumerable<Lop> GetLopByKhoa(int maKhoa)
        {
            return _context.Lop.Where(k=>k.MaKhoa==maKhoa).ToList();
        }

        public ICollection<Lop> GetLops()
        {
            return _context.Lop.ToList();
        }

        public bool LopExists(int maLop)
        {
            return _context.Lop.Any(l=>l.MaLop== maLop);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLop(Lop lop)
        {
            _context.Update(lop);
            return Save();
        }
    }
}
