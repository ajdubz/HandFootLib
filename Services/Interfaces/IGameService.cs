using HandFootLib.Models;
using HandFootLib.Models.DTOs.Game;
using HandFootLib.Models.DTOs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Services.Interfaces
{
    public interface IGameService
    {
        public GameDTO AddGame(GameAddDTO gameAddDTO);

        public void RemoveGame(int id);

        public IQueryable<GameDTO> GetGames();

        public IQueryable<GameTeamDTO> GetTeamsByGameId(int gameId);

        public IQueryable<GameRoundDTO> GetRoundsByGameId(int gameId);

        public void AddTeamToGame(int gameId, int teamId);
    }
}
