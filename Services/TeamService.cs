using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;

namespace HandFootLib.Services
{
    public class TeamService : ITeamService
    {
        private readonly Data _data = new();

        public TeamService(Data data) { _data = data; }

        public void AddPlayerToTeam(int playerId, int teamId)
        {
            var team = _data.Teams.Where(t => t.Id == teamId).FirstOrDefault();
            var player = _data.Players.Where(p => p.Id == playerId).FirstOrDefault();

            team.Players.Add(player);
            _data.Update(team);
            _data.SaveChanges();
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

        public IQueryable<TeamGetDTO> GetTeam(int id)
        {
            var allTeams = GetTeams();

            var team = allTeams.Where(t => t.Id == id);

            return team;
        }

        public IQueryable<TeamGetDTO> GetTeams()
        {
            var allTeams = from t in _data.Teams
                           join p in _data.Players
                           on t.Id equals p.TeamId
                           select new TeamGetDTO
                           {
                               Id = t.Id,
                               Name = t.Name,
                               PlayerId = p.Id,
                               PlayerNickName = p.NickName

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
