namespace HandFootLib.Models.DTOs.Team;

public class TeamGetWithPlayerNamesDTO
{
    public int? Id { get; set; } = 0;
    public string? Name { get; set; } = "";
    public List<string> PlayerNickNames { get; set; } = [];
}