using AutoMapper;
using QuanLySinhVien.Dto;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SinhVien, SinhVienDto>();
            CreateMap<SinhVienDto, SinhVien>(); 
            CreateMap<Lop, LopDto>();
            CreateMap<LopDto, Lop>();
            CreateMap<Khoa, KhoaDto>();
            CreateMap<KhoaDto, Khoa>();
            CreateMap<DiemThi, DiemThiDto>();
            CreateMap<DiemThiDto, DiemThi>();
            CreateMap<MonHoc, MonHocDto>();
            CreateMap<MonHocDto, MonHoc>();
        }
    }
}
