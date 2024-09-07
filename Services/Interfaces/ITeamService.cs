using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Services.Interfaces
{
    public interface ITeamService
    {
        public void AddTeam(TeamCreateDTO teamCreateDTO);
        public void AddPlayerToTeam(int playerId, int teamId);
        public IQueryable<TeamGetAllDTO> GetTeams();

        //public void RemoveTeam(int id);

        //public void UpdateTeam(TeamUpdateDTO teamUpdateDTO);

        //public void RemovePlayerFromTeam(int playerId, int teamId);

    }
}
