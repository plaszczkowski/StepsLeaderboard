using System.ComponentModel.DataAnnotations;

namespace StepsLeaderboard.Dto
{
    public class CreateCounterDto
    {
        [Required(ErrorMessage = "Employee name is required")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Steps are required")]
        [Range(0, int.MaxValue, ErrorMessage = "Steps must be a non-negative number")]
        public int Steps { get; set; }

        [Required(ErrorMessage = "Team ID is required")]
        public int TeamId { get; set; }
    }
}
