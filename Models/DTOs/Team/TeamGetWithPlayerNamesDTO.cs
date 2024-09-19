using HandFootLib.Models.DTOs.Player;

namespace HandFootLib.Models.DTOs.Team;

public class TeamGetWithPlayerNamesDTO
{
    public int? Id { get; set; } = 0;
    public string? Name { get; set; } = "";
    public List<PlayerGetBasicDTO> TeamMembers { get; set; } = [];
}