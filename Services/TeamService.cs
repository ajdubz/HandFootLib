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

        public void AddTeam(TeamCreateDTO teamCreateDTO)
        {
            try
            {
                var team = new Team
                {
                    Name = teamCreateDTO.Name,
                    //Players = GetPlayers(teamCreateDTO.PlayerIds).ToList()
                };

                _data.Teams.Add(team);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while adding team: {ex.Message}");
            }
        }
        
        public void RemoveTeam(int id)
        {
            try
            {
                var team = _data.Teams.SingleOrDefault(t => t.Id == id);

                if (team == null)
                {
                    Console.WriteLine("Team not found");
                    return;
                }

                _data.Teams.Remove(team);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while removing team: {ex.Message}");
            }
        }
        
        public void UpdateTeam(TeamUpdateDTO teamUpdateDTO)
        {
            try
            {
                var team = _data.Teams.SingleOrDefault(t => t.Id == teamUpdateDTO.Id);

                if (team == null)
                {
                    Console.WriteLine("Team not found");
                    return;
                }

                team.Name = teamUpdateDTO.Name;
                //team.Players = GetPlayers(teamUpdateDTO.PlayerIds).ToList();

                _data.Teams.Update(team);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while updating team: {ex.Message}");
            }
        }
        
        public void AddPlayerToTeam(int playerId, int teamId)
        {
            try
            {
                var player = _data.Players.SingleOrDefault(p => p.Id == playerId);
                var team = _data.Teams.SingleOrDefault(p => p.Id == teamId);

                if (player == null || team == null)
                {
                    Console.WriteLine("Player or Team not found");
                    return;
                };

                //team.Players.Add(player);

                _data.Players.Update(player);
                _data.Teams.Update(team);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while adding player to team: {ex.Message}");
            }
        }

        public void RemovePlayerFromTeam(int playerId, int teamId)
        {
            try
            {
                var player = _data.Players.SingleOrDefault(p => p.Id == playerId);
                var team = _data.Teams.SingleOrDefault(p => p.Id == teamId);

                if (player == null || team == null)
                {
                    Console.WriteLine("Player or Team not found");
                    return;
                }

                //team.Players.Remove(player);
                _data.Teams.Update(team);

                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while removing player from team: {ex.Message}");
            }
        }


        public TeamGetWithPlayers? GetTeam(int id)
        {
            try
            {
                var allTeams = GetTeams();
                var team = allTeams.SingleOrDefault(t => t.Id == id);
                return team;
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while getting team: {ex.Message}");
                return null;
            }
        }

        public IQueryable<TeamGetWithPlayers> GetTeams()
        {
            try
            {
                var allTeams = from t in _data.Teams
                               select new TeamGetWithPlayers
                               {
                                   Id = t.Id,
                                   Name = t.Name,
                               };

                return allTeams;
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while getting teams: {ex.Message}");
                return Enumerable.Empty<TeamGetWithPlayers>().AsQueryable();
            }
        }


        private IQueryable<Player> GetPlayers(List<int> playerIds)
        {
            try
            {
                if (playerIds?.Count < 1) { return new List<Player>().AsQueryable(); }

                var allPlayers = from p in _data.Players
                                 where playerIds.Contains(p.Id)
                                 select p;

                return allPlayers;
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while getting players: {ex.Message}");
                return Enumerable.Empty<Player>().AsQueryable();
            }
        }
    }
}
