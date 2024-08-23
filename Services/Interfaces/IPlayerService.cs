using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;

namespace HandFootLib.Services.Interfaces
{
    public interface IPlayerService
    {
        void AddPlayer(PlayerSetAccountDTO playerSetAccountDTO);

        void RemovePlayer(int id);

        void UpdatePlayerAccount(int playerId, PlayerSetAccountDTO playerSetAccountDTO);


        void AddFriend(int playerId, int friendId);

        void RemoveFriend(int playerId, int friendId);

        PlayerGetFullDetailsDTO? GetPlayer(int id);

        PlayerGetAccountDTO? GetPlayerAccount(int id);

        IQueryable<PlayerGetFullDetailsDTO> GetPlayers();
        
        IQueryable<PlayerGetAccountDTO> GetPlayersAccount();

        IQueryable<PlayerGetBasicDTO> GetPlayersBasic();

    }
}
