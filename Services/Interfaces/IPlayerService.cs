using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;
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

        public void RemovePlayer(int id);

        public PlayerGetWithFriendsDTO? GetPlayer(int id);

        public IQueryable<PlayerGetWithFriendsDTO> GetPlayers();

        public PlayerGetWithFriendsDTO GetPlayerWithFriends(int id);

        public IQueryable<PlayerGetWithFriendsDTO> GetPlayersWithFriends();

        public void AddFriend(int playerId, int friendId);

        public void RemoveFriend(int playerId, int friendId);

        public void UpdatePlayer(PlayerUpdateDTO playerUpdateDTO);
    }
}
