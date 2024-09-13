namespace QuanLySinhVien.Models
{
    public class DiemThi
    {
        public int MaMonHoc { get; set; }
        public MonHoc MonHoc { get; set; }  // Mỗi điểm thi thuộc về một môn học

        public int MaSV { get; set; }
        public SinhVien SinhVien { get; set; }  // Mỗi điểm thi thuộc về một sinh viên

        public double DiemLan1 { get; set; }
        public double DiemLan2 { get; set; }
    }
}
