using HandFootLib.Models;
using HandFootLib.Models.DTOs.Team;
using HandFootLib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using HandFootLib.Models.DTOs.Player;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Internal;

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

        public TeamCreateDTO AddTeam(TeamCreateDTO teamCreateDTO)
        {
            try
            {
                var team = new Team
                {
                    Name = teamCreateDTO.Name,
                };

                _data.Teams.Add(team);
                _data.SaveChanges();

                if (team.Id == 0)
                {
                    _logger.LogError($"Error in AddTeam, missing teamID:  {team.Id}");
                    throw new Exception("Missing teamID");

                }

                var newTeam = new TeamCreateDTO
                {
                    Id = team.Id,
                    Name = team.Name
                };

                //_logger.LogWarning(team.Id.ToString() + " - " + newTeam.Id.ToString());

                return newTeam;
            }
            catch (Exception ex)
            {
                // Handle the exception here
                _logger.LogError(ex, "Error in AddTeam");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public AddPlayersToTeamDTO AddPlayersToTeam(AddPlayersToTeamDTO addPlayersToTeam)
        {
            try
            {

                if (addPlayersToTeam.PlayerId1 == 0)
                {
                    _logger.LogError($"Error in AddPlayersToTeam, missing player1ID.. teamName: {addPlayersToTeam.TeamName}");
                    throw new Exception("Missing player1ID");
                }

                if (string.IsNullOrEmpty(addPlayersToTeam.TeamName))
                {
                    _logger.LogError($"Error in AddPlayersToTeam, missing teamName.. playerId1:  {addPlayersToTeam.PlayerId1}");
                    throw new Exception("Missing teamName");
                }

                var newTeam = new Team
                {
                    Name = addPlayersToTeam.TeamName,
                };

                _data.Teams.Add(newTeam);
                _data.SaveChanges();

                if (newTeam.Id == 0)
                {
                    _logger.LogError($"Error in AddPlayersToTeam, missing teamID.. teamName:  {addPlayersToTeam.TeamName}");
                    throw new Exception("Missing teamID");
                }


                var newPlayerTeam1 = new PlayerTeam
                {
                    PlayerId = addPlayersToTeam.PlayerId1,
                    TeamId = newTeam.Id,

                };

                _data.PlayerTeams.Add(newPlayerTeam1);


                if (addPlayersToTeam.PlayerId2 > 0)
                {
                    var newPlayerTeam2 = new PlayerTeam
                    {
                        PlayerId = addPlayersToTeam.PlayerId2,
                        TeamId = newTeam.Id,
                    };

                    _data.PlayerTeams.Add(newPlayerTeam2);
                }

                _data.SaveChanges();

                return new AddPlayersToTeamDTO
                {
                    PlayerId1 = addPlayersToTeam.PlayerId1,
                    PlayerId2 = addPlayersToTeam.PlayerId2,
                    TeamId = newTeam.Id,
                    TeamName = newTeam.Name,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in AddPlayerToTeam {addPlayersToTeam.TeamId}");
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

        public IQueryable<TeamGetWithPlayerNamesDTO> GetTeamsWithPlayerNames()
        {
            try
            {
                var playerTeams = from playerTeam in _data.PlayerTeams
                    join player in _data.Players on playerTeam.PlayerId equals player.Id
                    group player by playerTeam.TeamId into groupedPlayers
                    select new TeamGetWithPlayerNamesDTO { Id = groupedPlayers.Key, TeamMembers = groupedPlayers.Select(x => new PlayerGetBasicDTO
                    {
                        FullName = x.FullName,
                        Id = x.Id,
                        NickName = x.NickName
                    }).ToList() };


                var teamNames = from team in _data.Teams
                                select new TeamGetWithPlayerNamesDTO { Id = team.Id, Name = team.Name };


                var teamsWithNames = from team in teamNames
                                     join playerTeam in playerTeams on team.Id equals playerTeam.Id
                                     select new TeamGetWithPlayerNamesDTO { Id = team.Id, Name = team.Name, TeamMembers = playerTeam.TeamMembers};


                return teamsWithNames;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTeams");
                throw; // Rethrow the exception to propagate it to the caller

            }
        }

        public IQueryable<TeamGetWithPlayerNamesDTO> GetPlayerTeams(int inId)
        {
            try
            {
                var allTeams = GetTeamsWithPlayerNames();

                var teams = from team in allTeams
                            join playerTeam in _data.PlayerTeams on team.Id equals playerTeam.TeamId
                            where playerTeam.PlayerId == inId
                            select new TeamGetWithPlayerNamesDTO { Id = team.Id, Name = team.Name, TeamMembers = team.TeamMembers };
                return teams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTeams");
                throw; // Rethrow the exception to propagate it to the caller

            }

        }

        public IQueryable<TeamGetWithPlayerNamesDTO> GetTeamByPlayerIds(GetTeamByPlayerIds getTeamByPlayerIds)
        {
            try
            {
                var allTeams = GetTeamsWithPlayerNames();


                var teams1 = from playerTeam in _data.PlayerTeams
                            join team in allTeams on playerTeam.TeamId equals team.Id
                            where playerTeam.PlayerId == getTeamByPlayerIds.PlayerId1
                            select new TeamGetWithPlayerNamesDTO { Id = team.Id, Name = team.Name, TeamMembers = team.TeamMembers  };

                if (getTeamByPlayerIds.PlayerId2  == 0)
                {
                    return teams1;
                }

                var teams2 = from playerTeam in _data.PlayerTeams
                    join team in allTeams on playerTeam.TeamId equals team.Id
                    where playerTeam.PlayerId == getTeamByPlayerIds.PlayerId2
                    select new TeamGetWithPlayerNamesDTO { Id = team.Id, Name = team.Name, TeamMembers = team.TeamMembers };

                var sharedTeams = teams1.Intersect(teams2);


                return sharedTeams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTeamByPlayerIds");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public IQueryable<PlayerGetBasicDTO> GetPlayersByTeamId (int teamId)
        {
            try
            {

                var team = from pt in _data.PlayerTeams
                           join p in _data.Players on pt.PlayerId equals p.Id
                           where pt.TeamId == teamId
                           select new PlayerGetBasicDTO
                           {
                               Id = p.Id,
                               NickName = p.NickName,
                               FullName = p.FullName,
                           };

                return team;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPlayersByTeamId");
                throw;
            }
        }

    }
}
