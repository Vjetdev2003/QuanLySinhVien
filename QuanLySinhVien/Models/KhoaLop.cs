namespace QuanLySinhVien.Models
{
    public class KhoaLop
    {
        public int MaKhoa { get; set; }
        public int MaLop { get; set; }
        public Khoa Khoa { get; set; }
        public Lop Lop { get; set; }
    }
}
