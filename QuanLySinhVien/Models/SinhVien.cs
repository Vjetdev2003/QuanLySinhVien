namespace QuanLySinhVien.Models
{
    public class SinhVien
    {
        public int MaSV { get; set; }
        public string HoDem { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string NoiSinh { get; set; }

        // Foreign Key
        public int MaLop { get; set; }
        public Lop Lop { get; set; }  // Một sinh viên thuộc về một lớp

        // Một sinh viên có nhiều điểm thi
        public ICollection<DiemThi> DiemThi { get; set; }
        public ICollection<SinhVienLop> SinhVienLops { get; set; }
    }

}
