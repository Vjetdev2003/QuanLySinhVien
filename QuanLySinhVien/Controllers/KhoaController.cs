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
    public class KhoaController :Controller
    {
        private readonly IKhoaRepository _khoaRepository;
        private readonly IMapper _mapper;

        public KhoaController(IKhoaRepository khoaRepository,IMapper mapper)
        {
            _khoaRepository = khoaRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<Khoa>)))]
        public IActionResult GetKhoas()
        {
            var khoas = _mapper.Map<List<KhoaDto>>(_khoaRepository.GetKhoas());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(khoas);
        }
        [HttpGet("{khoaId}")]
        [ProducesResponseType(200, Type = typeof(Khoa))]
        [ProducesResponseType(400)]
        public IActionResult GetKhoa(int khoaId)
        {
            if (!_khoaRepository.KhoaExists(khoaId))
                return NotFound();
            var khoa = _mapper.Map<KhoaDto>(_khoaRepository.GetKhoa(khoaId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(khoa);
        }
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Khoa))]
        [ProducesResponseType(400)]
        public IActionResult CreateKhoa([FromBody] KhoaDto khoaCreate)
        {
            if (khoaCreate == null)
                return BadRequest("Khoa không hợp lệ");
            var khoaExists = _khoaRepository.GetKhoas().FirstOrDefault(k=>k.TenKhoa.Trim().ToUpper() == khoaCreate.TenKhoa.Trim().ToUpper());

            if(khoaExists != null)
            {
                ModelState.AddModelError("", "Khoa đã tồn tại.");
                return StatusCode(422, ModelState);
            }

            var khoaMap = _mapper.Map<Khoa>(khoaCreate);
            if (!_khoaRepository.CreateKhoa(khoaMap))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm ");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201, "Thêm thành công khoa");
        }
        [HttpPut("{maKhoa}")]
        [ProducesResponseType(204)] // Chỉ trả về 204 (No Content) khi thành công
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateKhoa(int maKhoa, [FromBody] KhoaDto khoaUpdate)
        {
            if (khoaUpdate == null)
                return BadRequest("Dữ liệu khoa cập nhật không thể null.");

            if (maKhoa != khoaUpdate.MaKhoa)
                return BadRequest("Mã khoa trong URL không khớp với mã khoa trong dữ liệu.");

            if (!_khoaRepository.KhoaExists(maKhoa))
                return NotFound("Khoa không tồn tại.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var khoaMap = _mapper.Map<Khoa>(khoaUpdate);

            if (!_khoaRepository.UpdateKhoa(khoaMap))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật khoa.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{maKhoa}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteKhoa(int maKhoa)
        {
            if (!_khoaRepository.KhoaExists(maKhoa))
                return NotFound("Khoa không tồn tại.");

            if (!_khoaRepository.DeleteKhoa(maKhoa))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa khoa.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }

    }
}
