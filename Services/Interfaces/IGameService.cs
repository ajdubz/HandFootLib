using HandFootLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Services.Interfaces
{
    public interface IGameService
    {
        public void AddGame(Game game);

        public void DeleteGame(int id);

        public Game GetGame(int id);

        public List<Game> GetGames();

        public void UpdateGame(Game game);
    }
}
