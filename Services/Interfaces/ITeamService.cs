using HandFootLib.Models;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Services.Interfaces
{
    public interface ITeamService
    {
        public void AddPlayerToTeam(int playerId, int teamId);

        public void AddTeam(TeamCreateDTO teamCreateDTO);

        public void RemoveTeam(int id);

        public TeamGetBasicDTO? GetTeam(int id);

        public IQueryable<TeamGetBasicDTO> GetTeams();

        public void RemovePlayerFromTeam(int playerId, int teamId);

        public void UpdateTeam(TeamUpdateDTO teamUpdateDTO);
    }
}
