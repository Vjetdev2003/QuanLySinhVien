using QuanLySinhVien.Models;
using System.Collections;

namespace QuanLySinhVien.Interfaces
{
    public interface IDiemThiRepository
    {
        IEnumerable<DiemThi> GetDiemThis();
        // Lấy điểm thi theo MaSV
        DiemThi GetDiemThi(int maMonHoc, int maSV);
        // Tạo điểm thi 
        bool CreateDiemThi(DiemThi diemThi);

        // Cập nhật điểm thi
        bool UpdateDiemThi(DiemThi diemThi);

        // Xóa điểm thi
        bool DeleteDiemThi(int maMonHoc, int maSV);

        // Kiểm tra điểm thi có tồn tại không
        bool DiemThiExists(int maMonHoc, int maSV);
        bool Save();
    }
}
