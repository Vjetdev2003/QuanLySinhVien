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
    public class DiemThiController : Controller
    {
        private readonly IDiemThiRepository _diemThiRepository;
        private readonly IMapper _mapper;

        public DiemThiController(IDiemThiRepository diemThiRepository,IMapper mapper)
        {
            _diemThiRepository = diemThiRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<DiemThi>)))]
        public IActionResult GetDiemThis()
        {
            var diemthis = _mapper.Map<List<DiemThiDto>>(_diemThiRepository.GetDiemThis());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(diemthis);
        }
        [HttpGet("{maSV}/{maMonHoc}")]
        [ProducesResponseType(200, Type = typeof(DiemThi))]
        [ProducesResponseType(400)]
        public IActionResult GetDiemThi(int maSV,int maMonHoc)
        {
            var diemThi = _diemThiRepository.GetDiemThi(maMonHoc, maSV);
            if (diemThi == null)
            {
                return NotFound();
            }

            var diemThiMap = _mapper.Map<DiemThiDto>(diemThi);
            return Ok(diemThiMap);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateDiemThi([FromBody] DiemThiDto diemThiCreate)
        {
            if (diemThiCreate == null)
                return BadRequest("Dữ liệu điểm thi không thể null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diemThi = _mapper.Map<DiemThi>(diemThiCreate);

            if (!_diemThiRepository.CreateDiemThi(diemThi))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm điểm thi.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return CreatedAtAction(nameof(GetDiemThi), new { maMonHoc = diemThi.MaMonHoc, maSV = diemThi.MaSV }, diemThiCreate);
        }
            [HttpPut("{maMonHoc}/{maSV}")]
        public IActionResult UpdateDiemThi(int maMonHoc, int maSV, [FromBody] DiemThiDto diemThiUpdate)
        {
            if (diemThiUpdate == null || maMonHoc != diemThiUpdate.MaMonHoc || maSV != diemThiUpdate.MaSV)
            {
                return BadRequest();
            }

            var diemThi = _diemThiRepository.GetDiemThi(maMonHoc, maSV);
            if (diemThi == null)
            {
                return NotFound();
            }

            _mapper.Map(diemThiUpdate, diemThi);
            if (!_diemThiRepository.UpdateDiemThi(diemThi))
            {
                ModelState.AddModelError("", "Có gì đó không ổn khi cập nhật điểm thi.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201,"Cập nhật điêm thành công");
        }
        [HttpDelete("{maMonHoc}/{maSV}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDiemThi(int maMonHoc, int maSV)
        {
            if (!_diemThiRepository.DiemThiExists(maMonHoc, maSV))
                return NotFound("Điểm thi không tồn tại.");

            if (!_diemThiRepository.DeleteDiemThi(maMonHoc, maSV))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa điểm thi.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
