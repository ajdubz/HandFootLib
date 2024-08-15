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
        void AddPlayer(Player player);

        void RemovePlayer(int id);

        void UpdatePlayer(PlayerUpdateDTO playerUpdateDTO);

        void AddFriend(int playerId, int friendId);

        void RemoveFriend(int playerId, int friendId);

        PlayerGetWithFriendsDTO? GetPlayer(int id);

        IQueryable<PlayerGetWithFriendsDTO> GetPlayers();

        PlayerGetWithFriendsDTO GetPlayerWithFriends(int id);

        IQueryable<PlayerGetWithFriendsDTO> GetPlayersWithFriends();
    }
}
