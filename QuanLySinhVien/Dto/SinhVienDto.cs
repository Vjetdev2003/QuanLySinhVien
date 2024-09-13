using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Dto
{
    public class SinhVienDto
    {
        [Required]
        public int MaSV { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Họ và tên đệm không được vượt quá 50 ký tự.")]
        public string HoDem { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Tên không được vượt quá 20 ký tự.")]
        public string Ten { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        public bool GioiTinh { get; set; }

        [StringLength(100, ErrorMessage = "Nơi sinh không được vượt quá 100 ký tự.")]
        public string NoiSinh { get; set; }

        [Required]
        public int MaLop { get; set; }
    }
}
