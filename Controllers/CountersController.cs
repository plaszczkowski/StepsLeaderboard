using Microsoft.AspNetCore.Mvc;
using StepsLeaderboard.Data;
using StepsLeaderboard.Dto;
using StepsLeaderboard.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace StepsLeaderboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountersController : ControllerBase
    {
        private readonly InMemoryDataStore _dataStore;

        public CountersController(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adds a new counter")]
        [SwaggerResponse(StatusCodes.Status201Created, "Counter created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        public ActionResult<CounterDto> PostCounter(CreateCounterDto createCounterDto)
        {
            var team = _dataStore.GetTeam(createCounterDto.TeamId);
            if (team == null)
            {
                return BadRequest("Invalid TeamId");
            }

            var counter = new Counter
            {
                EmployeeName = createCounterDto.EmployeeName,
                Steps = createCounterDto.Steps,
                TeamId = createCounterDto.TeamId,
                Team = team
            };

            _dataStore.AddCounter(counter);

            var counterDto = new CounterDto
            {
                Id = counter.Id,
                EmployeeName = counter.EmployeeName,
                Steps = counter.Steps,
                TeamId = counter.TeamId,
                TeamName = team.Name
            };

            return CreatedAtAction(nameof(GetCounter), new { id = counterDto.Id }, counterDto);
        }

        [HttpDelete("{id}")] public ActionResult DeleteCounter(int id) { var counter = _dataStore.GetCounter(id); if (counter == null) { return NotFound("Counter not found"); } _dataStore.DeleteCounter(id); return NoContent(); }

        [HttpGet("{id}")]
        public ActionResult<CounterDto> GetCounter(int id)
        {
            var counter = _dataStore.GetCounter(id);
            if (counter == null)
            {
                return NotFound();
            }

            var counterDto = new CounterDto
            {
                Id = counter.Id,
                EmployeeName = counter.EmployeeName,
                Steps = counter.Steps,
                TeamId = counter.TeamId,
                TeamName = counter.Team?.Name
            };

            return counterDto;
        }

        [HttpPatch("{id}/increment")]
        public ActionResult<CounterDto> IncrementCounter(int id, [FromBody] int steps)
        {
            var counter = _dataStore.GetCounter(id);
            if (counter == null)
            {
                return NotFound();
            }
            counter.Steps += steps;
            var counterDto = new CounterDto
            {
                Id = counter.Id,
                EmployeeName = counter.EmployeeName,
                Steps = counter.Steps,
                TeamId = counter.TeamId,
                TeamName = counter.Team?.Name
            };
            return Ok(counterDto);
        }
    }
}
