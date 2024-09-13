using QuanLySinhVien.Models;

namespace QuanLySinhVien.Interfaces
{
    public interface IKhoaRepository
    {
        Khoa GetKhoa(string name);
        Khoa GetKhoa(int maKhoa);
        ICollection<Khoa> GetKhoas();
        bool KhoaExists(int id);
        bool CreateKhoa(Khoa khoa);
        bool UpdateKhoa(Khoa khoa);
        bool DeleteKhoa(int maKhoa);
        bool Save();
    }
}
