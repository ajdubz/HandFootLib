using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Services.Interfaces
{
    public interface IPlayerService
    {
        public void AddPlayer(Player player);

        public void DeletePlayer(int id);

        public PlayerGetBasicDTO? GetPlayer(int id);

        public IQueryable<PlayerGetBasicDTO> GetPlayers();

        //public void AddFriend(int playerId, int friendId);

        //public void RemoveFriend(int playerId, int friendId);

        public void UpdatePlayer(Player player);
    }
}
