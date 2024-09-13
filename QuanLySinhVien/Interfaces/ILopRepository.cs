using QuanLySinhVien.Models;

namespace QuanLySinhVien.Interfaces
{
    public interface ILopRepository
    {
        ICollection<Lop> GetLops();
        Lop GetLop(int maLop);
        Lop GetLop(string name);
        IEnumerable<Lop> GetLopByKhoa(int maKhoa);

        bool LopExists(int maLop);
        bool CreateLop(Lop lop);
        bool UpdateLop(Lop lop);
        bool DeleteLop(int maLop);
        bool Save();

    }
}
