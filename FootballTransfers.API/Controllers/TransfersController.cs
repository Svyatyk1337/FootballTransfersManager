using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballTransfers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransfersController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransfersController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transfers = await _transferService.GetAllAsync();
            return Ok(transfers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transfer = await _transferService.GetByIdAsync(id);
            if (transfer == null) return NotFound();
            return Ok(transfer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransferDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _transferService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTransferDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _transferService.UpdateAsync(id, dto);
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
                await _transferService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
