using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;

namespace HandFootLib.Services
{
    public class TeamService : ITeamService
    {
        private readonly Data _data;
        private readonly IPlayerService _playerService;

        public TeamService(Data data, IPlayerService playerService)
        {
            _data = data;
            _playerService = playerService;
        }

        public void AddPlayerToTeam(int playerId, int teamId)
        {
            //var player = _playerService.GetPlayer(playerId);

            //var team = _data.Teams.Where(t => t.Id == teamId).FirstOrDefault();

            //team.Players.Add(player);
            //_data.Update(team);
            //_data.SaveChanges();
        }

        public void AddTeam(Team team)
        {
            _data.Add(team);
            _data.SaveChanges();
        }

        public void DeleteTeam(int id)
        {
            //var teams = _data.Teams.Where(t => t.Id == id);

            //var team = teams.FirstOrDefault();

            //foreach (var player in team.Players)
            //{
            //    team = RemovePlayerFromTeam(player.Id, team.Id);
            //}
            //_data.Teams.Remove(team);
            //_data.SaveChanges();
        }

        public TeamGetDTO GetTeam(int id)
        {
            var allTeams = GetTeams();

            var team = allTeams.SingleOrDefault(t => t.Id == id);

            return team;
        }

        //private IQueryable<PlayerGetBasicDTO> GetPlayers (Team team)
        //{
        //    var playerIds = team.PlayerIds;

        //    if (playerIds == null) { return new List<PlayerGetBasicDTO>().AsQueryable(); }

        //    var players = _data.Players.Where(p => playerIds.Contains(p.Id))
        //        .Select(p => new PlayerGetBasicDTO
        //        {
        //            Id = p.Id,
        //            NickName = p.NickName
        //        });

        //    return players;

        //}

        public IQueryable<TeamGetDTO> GetTeams()
        {
            var allTeams = from t in _data.Teams
                           join p in _data.Players
                           on t.Id equals p.TeamId
                           select new TeamGetDTO
                           {
                               Id = t.Id,
                               Name = t.Name,
                           };

            return allTeams;
        }

        //public List<TeamGetDTO> GetTeamsForGame(int gameId)
        //{
        //    var game = _data.Games.Where(g => g.Id == gameId).FirstOrDefault();

        //    return game.Teams.ToList();
        //}

        public void RemovePlayerFromTeam(int playerId, int teamId)
        { 
            //var allTeams = GetTeams();

            //var team = GetTeam(teamId).FirstOrDefault();

            //var allPlayers = PlayerService.GetPlayer(playerId);

            //var player = _data.Players.Where(p => p.Id == playerId).FirstOrDefault();

            //team.Players.Remove(player);
            //_data.Update(team);
            //_data.SaveChanges();

        }

        public void UpdateTeam(Team team)
        {

            _data.Update(team);
            _data.SaveChanges();
        }
    }
}
