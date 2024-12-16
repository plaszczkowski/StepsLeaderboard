using Microsoft.AspNetCore.Mvc;
using StepsLeaderboard.Data;
using StepsLeaderboard.Dto;
using StepsLeaderboard.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace StepsLeaderboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly InMemoryDataStore _dataStore;

        public TeamsController(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adds a new team")]
        [SwaggerResponse(StatusCodes.Status201Created, "Team created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        public ActionResult<Team> PostTeam([FromBody] TeamDto teamDto)
        {
            var team = new Team
            {
                Name = teamDto.Name
            };

            _dataStore.AddTeam(team);

            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a team")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Team deleted successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Team not found")]
        public ActionResult DeleteTeam(int id)
        {
            var team = _dataStore.GetTeam(id);
            if (team == null)
            {
                return NotFound("Team not found");
            }
            _dataStore.DeleteTeam(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a team")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Team not found")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = _dataStore.GetTeam(id);
            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        [HttpGet("{id}/totalSteps")]
        [SwaggerOperation(Summary = "Gets total steps in a team")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Team not found")]
        public ActionResult<int> GetTotalSteps(int id)
        {
            var team = _dataStore.GetTeam(id);
            if (team == null)
            {
                return NotFound("Team not found");
            }
            var totalSteps = team.Counters.Sum(counter => counter.Steps);
            return Ok(totalSteps);
        }

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Gets all teams with step counts")]
        public ActionResult<IEnumerable<TeamStepCountDto>> GetAllTeamsWithStepCounts()
        {
            var teams = _dataStore.GetTeams();
            var teamStepCounts = teams.Select(
                team => new TeamStepCountDto
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    TotalSteps = team.Counters.Sum(counter => counter.Steps)
                }).ToList();

            return Ok(teamStepCounts);
        }

        [HttpGet("{id}/counters")]
        [SwaggerOperation(Summary = "Gets counters in a team")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Team not found")]
        public ActionResult<IEnumerable<CounterDto>> GetCountersInTeam(int id)
        {
            var team = _dataStore.GetTeam(id);
            if (team == null)
            {
                return NotFound("Team not found");
            }
            var counters = team.Counters.Select(
                counter => new CounterDto
                {
                    Id = counter.Id,
                    EmployeeName = counter.EmployeeName,
                    Steps = counter.Steps,
                    TeamId = counter.TeamId,
                    TeamName = team.Name
                }).ToList();

            return Ok(counters);
        }
    }
}
