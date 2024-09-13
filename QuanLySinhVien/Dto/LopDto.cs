namespace QuanLySinhVien.Dto
{
    public class LopDto
    {
        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public string HeDaoTao { get; set; }
        public int NamNhapHoc { get; set; }
        public int SiSo { get; set; }

        // Foreign Key
        public int MaKhoa { get; set; }
    }
}
