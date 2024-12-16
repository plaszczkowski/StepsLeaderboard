using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StepsLeaderboard.Entities
{
    public class Team
    {
        public int Id { get; set; }
        [Required] 
        public string? Name { get; set; }
        public ICollection<Counter> Counters { get; set; } = new List<Counter>();
    }
}