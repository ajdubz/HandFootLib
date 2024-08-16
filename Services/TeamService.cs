using HandFootLib.Models;
using HandFootLib.Models.DTOs.Team;
using HandFootLib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using HandFootLib.Models.DTOs.Player;

namespace HandFootLib.Services
{
    public class TeamService : ITeamService
    {
        private readonly Data _data;

        public TeamService(Data data)
        {
            _data = data;
        }

        public void AddPlayerToTeam(int playerId, int teamId)
        {

            var player = _data.Players.SingleOrDefault(p => p.Id == playerId);
            var team = _data.Teams.SingleOrDefault(p => p.Id == teamId);

            if (player == null || team == null) return;

            player.Team = team;
            player.TeamId = team.Id;
            team.Players.Add(player);

            _data.Players.Update(player);
            _data.Teams.Update(team);
            _data.SaveChanges();
        }

        public void AddTeam(TeamCreateDTO teamCreateDTO)
        {
            var team = new Team
            {
                Name = teamCreateDTO.Name,
                Players = GetPlayers(teamCreateDTO.PlayerIds).ToList()
            };





            _data.Teams.Add(team);
            _data.SaveChanges();
        }

        public void RemoveTeam(int id)
        {
            var team = _data.Teams.Include(team => team.Players).SingleOrDefault(t => t.Id == id);

            if (team == null) return;

            var allPlayers = team.Players;

            foreach (var player in allPlayers)
            {

                player.TeamId = null;
                player.Team = null;
                _data.Players.Update(player);

            }

            _data.Teams.Remove(team);
            _data.SaveChanges();
        }

        public TeamGetWithPlayers? GetTeam(int id)
        {
            var allTeams = GetTeams();

            var team = allTeams.SingleOrDefault(t => t.Id == id);

            return team;
        }

        public IQueryable<TeamGetWithPlayers> GetTeams()
        {
            var allTeams = from t in _data.Teams
                           select new TeamGetWithPlayers
                           {
                               Id = t.Id,
                               Name = t.Name,
                               Players = (from p in t.Players
                                            select new PlayerGetBasicDTO
                                            {
                                                Id = p.Id,
                                                NickName = p.NickName
                                            }).ToList()
                           };

            return allTeams;
        }

        public void RemovePlayerFromTeam(int playerId, int teamId)
        {
            var player = _data.Players.SingleOrDefault(p => p.Id == playerId);
            var team = _data.Teams.SingleOrDefault(p => p.Id == teamId);

            if (player == null || team == null) return;

            player.Team = null;
            player.TeamId = null;
            _data.Players.Update(player);

            team.Players.Remove(player);
            _data.Teams.Update(team);

            _data.SaveChanges();
        }

        public void UpdateTeam(TeamUpdateDTO teamUpdateDTO)
        {
            var team = _data.Teams.SingleOrDefault(t => t.Id == teamUpdateDTO.Id);

            if (team == null) return;

            team.Name = teamUpdateDTO.Name;
            team.Players = GetPlayers(teamUpdateDTO.PlayerIds).ToList();





            _data.Teams.Update(team);
            _data.SaveChanges();
        }

        private IQueryable<Player> GetPlayers(List<int> playerIds)
        {
            if (playerIds?.Count < 1) { return new List<Player>().AsQueryable(); }

            var allPlayers = from p in _data.Players
                             where playerIds.Contains(p.Id)
                             select p;

            return allPlayers;

        }
    }
}
