using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;

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
    }
}
