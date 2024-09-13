using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;

namespace HandFootLib.Services.Interfaces
{
    public interface IPlayerService
    {
        PlayerSetAccountDTO AddPlayer(PlayerSetAccountDTO playerSetAccountDTO);

        void RemovePlayer(int id);

        void UpdatePlayerAccount(int playerId, PlayerSetAccountDTO playerSetAccountDTO);

        IQueryable<PlayerGetAllDTO> GetPlayers();


    }
}
