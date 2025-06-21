using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballTransfers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubsController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubsController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clubs = await _clubService.GetAllAsync();
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var club = await _clubService.GetByIdAsync(id);
            if (club == null) return NotFound();
            return Ok(club);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _clubService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateClubDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _clubService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clubService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
