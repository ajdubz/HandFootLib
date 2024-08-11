using HandFootLib.Models;
using HandFootLib.Models.DTOs;

namespace HandFootLib.Services.Interfaces
{
    public interface ITeamService
    {
        public void AddPlayerToTeam(int playerId, int teamId);

        public void AddTeam(Team team);

        public void DeleteTeam(int id);

        public IQueryable<TeamGetDTO> GetTeam(int id);

        public IQueryable<TeamGetDTO> GetTeams();

        //public List<TeamGetDTO> GetTeamsForGame(int gameId);

        public void RemovePlayerFromTeam(int playerId, int teamId);

        public void UpdateTeam(Team team);
    }
}
