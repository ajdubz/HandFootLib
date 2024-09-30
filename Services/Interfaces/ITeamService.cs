using HandFootLib.Models;
using HandFootLib.Models.DTOs.Game;
using HandFootLib.Models.DTOs.Player;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Services.Interfaces
{
    public interface ITeamService
    {
        public TeamCreateDTO AddTeam(TeamCreateDTO teamCreateDTO);
        public AddPlayersToTeamDTO AddPlayersToTeam(AddPlayersToTeamDTO addPlayersToTeam);
        public IQueryable<TeamGetAllDTO> GetTeams();
        public IQueryable<TeamGetWithPlayerNamesDTO> GetTeamsWithPlayerNames();
        public IQueryable<TeamGetWithPlayerNamesDTO> GetTeamByPlayerIds(GetTeamByPlayerIds getTeamByPlayerIds);
        public IQueryable<TeamGetWithPlayerNamesDTO> GetPlayerTeams(int inId);
        public IQueryable<PlayerGetBasicDTO> GetPlayersByTeamId(int teamId);
        public IQueryable<GameRoundDTO> GetRoundsByTeamId(int gameTeamId);



    }
}
