using StepsLeaderboard.Dto;
using StepsLeaderboard.Entities;

public static class Converter
{
    public static CounterDto ToDto(Counter counter)
    {
        var CounterDto = new CounterDto();
        CounterDto .Id = counter.Id;
        CounterDto .EmployeeName = counter.EmployeeName;
        CounterDto .Steps = counter.Steps;
        CounterDto .TeamId = counter.TeamId;
        return CounterDto;
    }

    public static Counter ToEntity(CounterDto counterDto)
    {
        var Entity = new Counter();
        Entity.Id = counterDto.Id;
        Entity.EmployeeName = counterDto.EmployeeName;
        Entity.Steps = counterDto.Steps;
        Entity.TeamId = counterDto.TeamId;
        return Entity;
    }

    public static TeamDto ToDto(Team team)
    {
        var Entity = new TeamDto();
        Entity.Id = team.Id;
        Entity.Name = team.Name;
        Entity.Counters = team.Counters?.Select(ToDto).ToList();
        return Entity;
    }

    public static Team ToEntity(TeamDto teamDto)
    {
        var Entity = new Team();
        Entity.Id = teamDto.Id;
        Entity.Name = teamDto.Name;
        Entity.Counters = teamDto.Counters?.Select(ToEntity).ToList() ?? new List<Counter>();
        return Entity;
    }
}