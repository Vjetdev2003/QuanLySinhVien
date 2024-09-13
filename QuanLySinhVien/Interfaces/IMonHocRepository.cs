using QuanLySinhVien.Models;

namespace QuanLySinhVien.Interfaces
{
    public interface IMonHocRepository
    {
        MonHoc GetMonHoc(int maMonHoc);
        MonHoc GetMonHoc(string name);
        ICollection<MonHoc> GetMonHocs();
        bool CreateMonHoc(MonHoc monHoc);
        bool UpdateMonHoc(MonHoc monHoc);
        bool DeleteMonHoc(MonHoc monHoc);
        bool MonHocExists (int maMonHoc);
        bool Save();
    }
}
