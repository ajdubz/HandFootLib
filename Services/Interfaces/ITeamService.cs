using HandFootLib.Models;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Services.Interfaces
{
    public interface ITeamService
    {
        public void AddPlayerToTeam(int playerId, int teamId);

        public void AddTeam(TeamCreateDTO teamCreateDTO);

        public void RemoveTeam(int id);

        public TeamGetWithPlayers? GetTeam(int id);

        public IQueryable<TeamGetWithPlayers> GetTeams();

        public void RemovePlayerFromTeam(int playerId, int teamId);

        public void UpdateTeam(TeamUpdateDTO teamUpdateDTO);
    }
}
