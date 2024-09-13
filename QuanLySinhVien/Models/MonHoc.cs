namespace QuanLySinhVien.Models
{
    public class MonHoc
    {
        public int MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int SoDVHT { get; set; }

        // Một môn học có nhiều điểm thi
        public ICollection<DiemThi> DiemThi { get; set; }
    }
}
