using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Interfaces;
using FootballTransfers.Application.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace FootballTransfers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agents = await _agentService.GetAllAsync();
            return Ok(agents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agent = await _agentService.GetByIdAsync(id);
            if (agent == null) return NotFound();
            return Ok(agent);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAgentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _agentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAgentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _agentService.UpdateAsync(id, dto);
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
                await _agentService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] AgentFilterParams filterParams)
        {
            var result = await _agentService.GetPagedAsync(filterParams);
            return Ok(result);
        }
    }
}
