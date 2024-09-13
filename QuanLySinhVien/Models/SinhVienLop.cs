namespace QuanLySinhVien.Models
{
    public class SinhVienLop
    {
        public int MaSV {  get; set; }
        public int MaLop {  get; set; }
        public SinhVien SinhVien { get; set; }
        public Lop Lop { get; set; }
    }
}
