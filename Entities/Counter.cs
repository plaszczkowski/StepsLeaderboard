using System.ComponentModel.DataAnnotations;

namespace StepsLeaderboard.Entities
{
    public class Counter
    {
        public int Id { get; set; }
        [Required] 
        public string? EmployeeName { get; set; }
        [Required] 
        public int Steps { get; set; }
        public int TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
