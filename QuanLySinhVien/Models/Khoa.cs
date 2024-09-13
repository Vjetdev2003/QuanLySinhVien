namespace QuanLySinhVien.Models
{
    public class Khoa
    {
        public int MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string DienThoai { get; set; }

        // Một khoa có nhiều lớp
        public ICollection<Lop> Lops { get; set; }
        public ICollection<KhoaLop> KhoaLops { get; set; }
    }
}
