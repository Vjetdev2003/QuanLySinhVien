using QuanLySinhVien.Models;

namespace QuanLySinhVien.Interfaces
{
    public interface ISinhVienRepository
    {
        SinhVien GetSinhVien(int id);
        SinhVien GetSinhVien(string name);

        ICollection<SinhVien> GetSinhViens();
        IEnumerable<SinhVien> GetSinhVienByLop(int maLop);
        bool SinhVienExists(int sinhvienId);
        bool CreateSinhVien(SinhVien sv);
        bool UpdateSinhVien(SinhVien sinhVien);
        bool DeleteSinhVien(int maSV);
        bool Save();
    }
}
