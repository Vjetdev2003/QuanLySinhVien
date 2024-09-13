using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.Dto;
using QuanLySinhVien.Interfaces;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonHocController : Controller
    {
        private readonly IMonHocRepository _monHocRepository;
        private readonly IMapper _mapper;

        public MonHocController(IMonHocRepository monHocRepository, IMapper mapper)
        {
            _monHocRepository = monHocRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MonHoc>))]
        public IActionResult GetMonHocs() {

            var monHocs = _mapper.Map<List<MonHocDto>>(_monHocRepository.GetMonHocs());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(monHocs);
        }
        [HttpGet("{maMonHoc}")]
        [ProducesResponseType(200, Type = typeof(MonHoc))]
        [ProducesResponseType(404)]
        public IActionResult GetMonHoc(int maMonHoc)
        {
            if (!_monHocRepository.MonHocExists(maMonHoc))
                return NotFound();

            var monHoc = _mapper.Map<MonHocDto>(_monHocRepository.GetMonHoc(maMonHoc));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(monHoc);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateMonHoc([FromBody] MonHocDto monHocCreate)
        {
            if (monHocCreate == null)
                return BadRequest(ModelState);

            var monHoc = _monHocRepository.GetMonHocs()
                .FirstOrDefault(m => m.TenMonHoc.Trim().ToUpper() == monHocCreate.TenMonHoc.Trim().ToUpper());

            if (monHoc != null)
            {
                ModelState.AddModelError("", "Môn học đã tồn tại.");
                return StatusCode(422, ModelState);
            }

            var monHocMap = _mapper.Map<MonHoc>(monHocCreate);

            if (!_monHocRepository.CreateMonHoc(monHocMap))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi tạo mới môn học.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Tạo mới thành công");
        }
        [HttpPut("{maMonHoc}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMonHoc(int maMonHoc, [FromBody] MonHocDto monHocUpdate)
        {
            if (monHocUpdate == null)
                return BadRequest(ModelState);

            if (maMonHoc != monHocUpdate.MaMonHoc)
                return BadRequest(ModelState);

            if (!_monHocRepository.MonHocExists(maMonHoc))
                return NotFound();

            var monHocMap = _mapper.Map<MonHoc>(monHocUpdate);

            if (!_monHocRepository.UpdateMonHoc(monHocMap))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật môn học.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{maMonHoc}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMonHoc(int maMonHoc)
        {
            if (!_monHocRepository.MonHocExists(maMonHoc))
                return NotFound();

            var monHoc = _monHocRepository.GetMonHoc(maMonHoc);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_monHocRepository.DeleteMonHoc(monHoc))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa môn học.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
