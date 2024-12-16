using System.ComponentModel.DataAnnotations;

namespace StepsLeaderboard.Dto
{
    public class TeamDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<CounterDto> Counters { get; set; } = new List<CounterDto>();
    }
}
