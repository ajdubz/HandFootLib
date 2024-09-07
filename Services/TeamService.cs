using HandFootLib.Models;
using HandFootLib.Models.DTOs.Team;
using HandFootLib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using HandFootLib.Models.DTOs.Player;
using Microsoft.Extensions.Logging;

namespace HandFootLib.Services
{
    public class TeamService : ITeamService
    {
        private readonly Data _data;
        private readonly ILogger<TeamService> _logger;

        public TeamService(Data data, ILogger<TeamService> logger)
        {
            _data = data;
            _logger = logger;
        }

        public void AddTeam(TeamCreateDTO teamCreateDTO)
        {
            try
            {
                var team = new Team
                {
                    Name = teamCreateDTO.Name,
                };

                _data.Teams.Add(team);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here
                _logger.LogError(ex, "Error in AddTeam");
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
                    _logger.LogError("Error in AddPlayerToTeam, team or player not found");
                    return;
                };

                _data.Players.Update(player);
                _data.Teams.Update(team);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddPlayerToTeam");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public IQueryable<TeamGetAllDTO> GetTeams()
        {
            try
            {
                var teams = from team in _data.Teams
                            select new TeamGetAllDTO { Id = team.Id, Name = team.Name };
                return teams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTeams");
                throw; // Rethrow the exception to propagate it to the caller

            }

        }

        //public void RemovePlayerFromTeam(int playerId, int teamId)
        //{
        //    try
        //    {
        //        var player = _data.Players.SingleOrDefault(p => p.Id == playerId);
        //        var team = _data.Teams.SingleOrDefault(p => p.Id == teamId);

        //        if (player == null || team == null)
        //        {
        //            Console.WriteLine("Player or Team not found");
        //            return;
        //        }

        //        //team.Players.Remove(player);
        //        _data.Teams.Update(team);

        //        _data.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception here
        //        Console.WriteLine($"An error occurred while removing player from team: {ex.Message}");
        //    }
        //}


        //public void RemoveTeam(int id)
        //{
        //    try
        //    {
        //        var team = _data.Teams.SingleOrDefault(t => t.Id == id);

        //        if (team == null)
        //        {
        //            Console.WriteLine("Team not found");
        //            return;
        //        }

        //        _data.Teams.Remove(team);
        //        _data.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception here
        //        Console.WriteLine($"An error occurred while removing team: {ex.Message}");
        //    }
        //}

        //public void UpdateTeam(TeamUpdateDTO teamUpdateDTO)
        //{
        //    try
        //    {
        //        var team = _data.Teams.SingleOrDefault(t => t.Id == teamUpdateDTO.Id);

        //        if (team == null)
        //        {
        //            Console.WriteLine("Team not found");
        //            return;
        //        }

        //        team.Name = teamUpdateDTO.Name;
        //        //team.Players = GetPlayers(teamUpdateDTO.PlayerIds).ToList();

        //        _data.Teams.Update(team);
        //        _data.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception here
        //        Console.WriteLine($"An error occurred while updating team: {ex.Message}");
        //    }
        //}

    }
}
