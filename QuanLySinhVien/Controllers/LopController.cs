using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.Dto;
using QuanLySinhVien.Interfaces;
using QuanLySinhVien.Models;
using QuanLySinhVien.Repository;

namespace QuanLySinhVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopController : Controller
    {
        private readonly ILopRepository _lopRepository;
        private readonly IMapper _mapper;

        public LopController(ILopRepository lopRepository, IMapper mapper)
        {
            _lopRepository = lopRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<Lop>)))]
        public IActionResult GetLops()
        {
            var lops = _mapper.Map<List<LopDto>>(_lopRepository.GetLops());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(lops);
        }
        [HttpGet("{lopId}")]
        [ProducesResponseType(200, Type = typeof(Lop))]
        [ProducesResponseType(400)]
        public IActionResult GetLop(int lopId)
        {
            if (!_lopRepository.LopExists(lopId))
                return NotFound();
            var lop = _mapper.Map<LopDto>(_lopRepository.GetLop(lopId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lop);
        }
        [HttpGet("khoa/{maKhoa}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lop>))]
        [ProducesResponseType(400)]
        public IActionResult GetSinhVienByLop(int maKhoa)
        {
            var khoas = _lopRepository.GetLopByKhoa(maKhoa);

            if (khoas == null || !khoas.Any())
                return NotFound("Không tìm thấy sinh viên cho lớp này.");

            // Ánh xạ danh sách sinh viên sang danh sách DTO
            var lopMap = _mapper.Map<List<LopDto>>(khoas);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lopMap);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult CreateLop([FromBody] LopDto lopCreate) 
        {
            if (lopCreate == null)
                return BadRequest("Dữ liệu lớp không hợp lệ");
            var extistsLop = _lopRepository.GetLops()
                .FirstOrDefault(c => c.TenLop.Trim().ToUpper() == lopCreate.TenLop.Trim().ToUpper());
            if (extistsLop != null)
            {
                ModelState.AddModelError("", "Lớp đã tồn tại.");
                return StatusCode(422, ModelState);
            }
            var lopMap = _mapper.Map<Lop>(lopCreate);

            if (!_lopRepository.CreateLop(lopMap)) {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu lớp.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Thêm Lớp thành công.");

        }
        [HttpPut("{maLop}")]
        [ProducesResponseType(204)] // Chỉ trả về 204 (No Content) khi thành công
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLop(int maLop, [FromBody] LopDto lopUpdate)
        {
            if (lopUpdate == null)
                return BadRequest("Dữ liệu lớp cập nhật không thể null.");

            if (maLop != lopUpdate.MaLop)
                return BadRequest("Mã lớp trong URL không khớp với mã lớp trong dữ liệu.");

            if (!_lopRepository.LopExists(maLop))
                return NotFound("Lớp không tồn tại.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lopMap = _mapper.Map<Lop>(lopUpdate);

            if (!_lopRepository.UpdateLop(lopMap))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật lớp.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{maLop}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteLop(int maLop)
        {
            if (!_lopRepository.LopExists(maLop))
                return NotFound("Lớp không tồn tại.");

            if (!_lopRepository.DeleteLop(maLop))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa lớp.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
