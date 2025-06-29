using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Interfaces;
using FootballTransfers.Application.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace FootballTransfers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var players = await _playerService.GetAllAsync();
            return Ok(players); // 200
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var player = await _playerService.GetByIdAsync(id);
            if (player == null) return NotFound(); // 404
            return Ok(player); // 200
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // 400
            var created = await _playerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created); // 201
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePlayerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // 400
            try
            {
                await _playerService.UpdateAsync(id, dto);
                return NoContent(); // 204
            }
            catch (Exception e)
            {
                return NotFound(e.Message); // 404 or 500 depending on the message
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _playerService.DeleteAsync(id);
                return NoContent(); // 204
            }
            catch (Exception e)
            {
                return NotFound(e.Message); // 404 or 500
            }
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PlayerFilterParams filterParams)
        {
            var result = await _playerService.GetPagedAsync(filterParams);
            return Ok(result);
        }

    }
}