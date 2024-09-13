using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.Dto;
using QuanLySinhVien.Interfaces;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienController : Controller
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IMapper _mapper;
        private readonly ILopRepository _lopRepository;

        public SinhVienController(ISinhVienRepository sinhVienRepository, IMapper mapper,ILopRepository lopRepository)
        {
            _sinhVienRepository = sinhVienRepository;
            _mapper = mapper;
            _lopRepository = lopRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SinhVien>))]
        public IActionResult GetSinhVien()
        {

            var sinhViens = _mapper.Map<List<SinhVienDto>>(_sinhVienRepository.GetSinhViens());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sinhViens);
        }
        [HttpGet("{sinhVienId}")]
        [ProducesResponseType(200, Type = typeof(SinhVien))]
        [ProducesResponseType(400)]
        public IActionResult GetSinhVien(int sinhVienId)
        {
            if (!_sinhVienRepository.SinhVienExists(sinhVienId))
                return NotFound();
            var sinhViens = _mapper.Map<SinhVienDto>(_sinhVienRepository.GetSinhVien(sinhVienId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sinhViens);
        }
        [HttpGet("lop/{maLop}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SinhVien>))]
        [ProducesResponseType(400)]
        public IActionResult GetSinhVienByLop(int maLop)
        {
            var sinhViens = _sinhVienRepository.GetSinhVienByLop(maLop);

            if (sinhViens == null || !sinhViens.Any())
                return NotFound("Không tìm thấy sinh viên cho lớp này.");

            // Ánh xạ danh sách sinh viên sang danh sách DTO
            var sinhVienMap = _mapper.Map<List<SinhVienDto>>(sinhViens);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sinhVienMap);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateSinhVien([FromBody] SinhVienDto sinhVienCreate)
        {
            if (sinhVienCreate == null)
                return BadRequest("Dữ liệu sinh viên không hợp lệ.");

            // Kiểm tra xem sinh viên đã tồn tại chưa
            var existingSinhVien = _sinhVienRepository.GetSinhViens()
                .FirstOrDefault(c => c.Ten.Trim().ToUpper() == sinhVienCreate.Ten.Trim().ToUpper());

            if (existingSinhVien != null)
            {
                ModelState.AddModelError("", "Sinh viên đã tồn tại.");
                return StatusCode(422, ModelState);
            }

            // Kiểm tra xem lớp học có tồn tại không
            var lop = _lopRepository.GetLops()
                .FirstOrDefault(l => l.MaLop == sinhVienCreate.MaLop);

            if (lop == null)
            {
                ModelState.AddModelError("", "Lớp học không tồn tại.");
                return StatusCode(404, ModelState);
            }

            // Ánh xạ từ DTO sang entity
            var sinhVienMap = _mapper.Map<SinhVien>(sinhVienCreate);

            // Tạo sinh viên mới
            if (!_sinhVienRepository.CreateSinhVien(sinhVienMap))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu sinh viên.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Thêm thành công.");
        }
        [HttpPut("{maSV}")]
        [ProducesResponseType(204)] // Chỉ trả về 204 (No Content) khi thành công
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSinhVien(int maSV, [FromBody] SinhVienDto sinhVienUpdate)
        {
            if (sinhVienUpdate == null)
                return BadRequest("Dữ liệu sinh viên cập nhật không thể null.");

            if (maSV != sinhVienUpdate.MaSV)
                return BadRequest("Mã sinh viên trong URL không khớp với mã sinh viên trong dữ liệu.");

            if (!_sinhVienRepository.SinhVienExists(maSV))
                return NotFound("Sinh viên không tồn tại.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sinhVienMap = _mapper.Map<SinhVien>(sinhVienUpdate);

            if (!_sinhVienRepository.UpdateSinhVien(sinhVienMap))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sinh viên.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent(); 
        }
        [HttpDelete("{maSV}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteSinhVien(int maSV)
        {
            if (!_sinhVienRepository.SinhVienExists(maSV))
                return NotFound("Sinh viên không tồn tại.");

            if (!_sinhVienRepository.DeleteSinhVien(maSV))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa sinh viên.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
