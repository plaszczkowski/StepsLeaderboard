using System.ComponentModel.DataAnnotations;

namespace StepsLeaderboard.Dto
{
    public class CreateTeamDto
    {
        [Required]
        public string Name { get; set; }
    }
}
