using StepsLeaderboard.Dto;
using StepsLeaderboard.Entities;
using System.ComponentModel.DataAnnotations;

namespace StepsLeaderboard.Dto
{
    public class CounterDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public int Steps { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}