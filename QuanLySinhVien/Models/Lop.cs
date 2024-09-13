namespace QuanLySinhVien.Models
{
    public class Lop
    {
        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public string HeDaoTao { get; set; }
        public int NamNhapHoc { get; set; }
        public int SiSo { get; set; }

        // Foreign Key
        public int MaKhoa { get; set; }
        public Khoa Khoa { get; set; }  // Mỗi lớp thuộc về một khoa

        // Một lớp có nhiều sinh viên
        public ICollection<SinhVien> SinhViens { get; set; }
        public ICollection<KhoaLop> KhoaLops { get; set; }
        public ICollection<SinhVienLop> SinhVienLops { get; set; }

    }
}
