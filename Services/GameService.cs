using HandFootLib.Models;
using HandFootLib.Services.Interfaces;
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

        public GameService(Data data) { _data = data; }

        public void AddGame(Game game)
        {
            _data.Games.Add(game);
            _data.SaveChanges();

        }

        public void DeleteGame(int id)
        {
            var game = _data.Games.Where(g => g.Id == id).FirstOrDefault();

            _data.Games.Remove(game);
            _data.SaveChanges();
        }

        public Game GetGame(int id)
        {
            return _data.Games.Where(g => g.Id == id).FirstOrDefault();
        }

        public List<Game> GetGames()
        {
            return _data.Games.ToList();
        }

        public void UpdateGame(Game game)
        {
            _data.Update(game);
            _data.SaveChanges();

        }
    }
}
