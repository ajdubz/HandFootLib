using Azure.Core;
using HandFootLib.Models;
using HandFootLib.Models.DTOs.Game;
using HandFootLib.Models.DTOs.Rules;
using HandFootLib.Models.DTOs.Team;
using HandFootLib.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Services
{
    public class GameService : IGameService
    {
        private Data _data = new Data();
        private readonly ILogger<GameService> _logger;
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;

        public GameService(Data data, ILogger<GameService> logger, IPlayerService playerService, ITeamService teamService) 
        { 
            _data = data; 
            _logger = logger; 
            _playerService = playerService;
            _teamService = teamService;
        }



        public GameDTO AddGame(GameAddDTO gameAddDTO)
        {
            try
            {
                var newGame = new Game
                {
                    Date = gameAddDTO.Date ?? DateTime.Now
                };

                _data.Games.Add(newGame);
                _data.SaveChanges();

                var tempGame = new GameDTO
                {
                    Id = newGame.Id,
                    Date = newGame.Date,
                    Rules = GameRules(newGame),
                };

                return tempGame;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddGame");
                throw;
            }
        }

        public IQueryable<GameDTO> GetGames()
        {
            try
            {
                var games = from g in _data.Games
                            select new GameDTO
                            {
                                Id = g.Id,
                                Date = g.Date,
                                Rules = GameRules(g),
                            };

                return games;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetGames");
                throw;
            }

        }

        public IQueryable<GameTeamDTO> GetTeamsByGameId (int gameId)
        {
            try
            {
                //_logger.LogWarning(gameId.ToString());
                var currGame = GetGames().SingleOrDefault(g => g.Id == gameId);

                if (currGame == null)
                {
                    _logger.LogError("Game not found");
                    throw new Exception("Game not found");
                }
                // Enumerate the results of the first query to avoid open DataReader issues

                var gameTeamsList = (from gt in _data.GameTeams
                                     join t in _data.Teams on gt.TeamId equals t.Id
                                     where gt.GameId == gameId
                                     select new
                                     {
                                         GameTeam = gt,
                                         Team = t
                                     }).ToList();

                var gameTeams = gameTeamsList.Select(gt => new GameTeamDTO
                {
                    Game = currGame,
                    Team = new TeamGetWithPlayerNamesDTO
                    {
                        Id = gt.Team.Id,
                        Name = gt.Team.Name,
                        TeamMembers = _teamService.GetPlayersByTeamId(gt.Team.Id).ToList(),
                    },
                }).AsQueryable();

                return gameTeams;

                //var gameTeams = from gt in _data.GameTeams
                //                join t in _data.Teams on gt.TeamId equals t.Id
                //                where gt.GameId == gameId
                //                select new GameTeamDTO
                //                {
                //                    Game = currGame,
                //                    Team = new TeamGetWithPlayerNamesDTO
                //                    {
                //                        Id = t.Id,
                //                        Name = t.Name,
                //                        TeamMembers = _teamService.GetPlayersByTeamId(t.Id).ToList(),
                //                    },
                //                };



                return gameTeams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTeamsByGameId");
                throw;
            }
        }

        public IQueryable<GameRoundDTO> GetRoundsByGameId(int gameId)
        {
            try
            {
                var currGame = GetGames().SingleOrDefault(g => g.Id == gameId);
                if (currGame == null)
                {
                    _logger.LogError("Game not found");
                    throw new Exception("Game not found");
                }

                var gameRounds = from gr in _data.GameRounds
                                 join gt in _data.GameTeams on gr.GameTeamId equals gt.Id
                                 where gt.GameId == gameId
                                 select new GameRoundDTO
                                 {
                                     Id = gr.Id,
                                     GameTeam = new GameTeamDTO
                                     {
                                         Id = gt.Id,
                                         Game = currGame,
                                         Team = new TeamGetWithPlayerNamesDTO
                                         {
                                             Id = gt.Team != null ? gt.Team.Id : 0,
                                             Name = gt.Team != null ? gt.Team.Name : "",
                                             //TeamMembers = _playerService.GetFriends(gt.Team != null ? gt.Team.Id : 0).ToList(),
                                         },
                                     },
                                     RoundNumber = gr.RoundNumber,
                                     HandScore = gr.HandScore,
                                     CleanBooks = gr.CleanBooks,
                                     DirtyBooks = gr.DirtyBooks,
                                     RedThrees = gr.RedThrees,
                                     PulledCorrect = gr.PulledCorrect,
                                     IsWinner = gr.IsWinner,
                                 };

                return gameRounds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRoundsByGameId");
                throw;
            }
        }


        public void RemoveGame(int id)
        {
            try
            {
                var game = _data.Games.SingleOrDefault(g => g.Id == id);
                if (game == null)
                {
                    throw new Exception("Game not found");
                }


                _data.Games.Remove(game);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RemoveGame");
                throw;
            }
        }

        public void AddTeamToGame(int gameId, int teamId)
        {
            try
            {
                var newGameTeam = new GameTeam
                {
                    GameId = gameId,
                    TeamId = teamId,
                };

                _data.GameTeams.Add(newGameTeam);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding team to game");
                throw;
            }
        }

        private static RulesGetDTO GameRules(Game inGame)
        {
            var rules = new RulesGetDTO
            {
                CardsToStart = inGame.CardsToStart ?? 26,
                CardsToDraw = inGame.CardsToDraw ?? 3,
                WinnerScore = inGame.WinnerScore ?? 100,
                CleanBookScore = inGame.CleanBookScore ?? 500,
                DirtyBookScore = inGame.DirtyBookScore ?? 300,
                PulledScore = inGame.PulledScore ?? 100,
                RedThreeScore = inGame.RedThreeScore ?? 300,
                RoundThresholds = inGame.RoundThresholds ?? new int[] { 60, 90, 120, 150 },
            };

            return rules;

        }
    }
}
