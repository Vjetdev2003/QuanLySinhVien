using AutoMapper;
using QuanLySinhVien.Data;
using QuanLySinhVien.Interfaces;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Repository
{
    public class KhoaRepository : IKhoaRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public KhoaRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateKhoa(Khoa khoa)
        {
            _context.Add(khoa);
            return Save();

        }

        public bool DeleteKhoa(int maKhoa)
        {
            _context.Remove(maKhoa);
            return Save();
        }

        public ICollection<Khoa> GetKhoas()
        {
           return _context.Khoa.ToList();
        }

        public Khoa GetKhoa(string name)
        {
            return _context.Khoa.Where(k => k.TenKhoa == name).FirstOrDefault();
        }

        public Khoa GetKhoa(int maKhoa)
        {
            return _context.Khoa.Where(k => k.MaKhoa == maKhoa).FirstOrDefault();

        }

        public bool KhoaExists(int id)
        {
            return _context.Khoa.Any(k=>k.MaKhoa==id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ? true : false;
        }

        public bool UpdateKhoa(Khoa khoa)
        {
            _context.Update(khoa);
            return Save();
        }
    }
}
