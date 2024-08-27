using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;

namespace HandFootLib.Services.Interfaces
{
    public interface IFriendsService
    {
        void AddFriendRequest(int playerId, int friendId);

        void RemoveFriendRequest(int playerId, int friendId);

        void AcceptFriendRequest(int playerId, int friendId);

        void DeclineFriendRequest(int playerId, int friendId);

        IQueryable<PlayerGetBasicDTO> GetFriends(int playerId);

        IQueryable<PlayerGetBasicDTO> GetFriendRequests(int playerId);

        IQueryable<PlayerGetBasicDTO> GetFriendRequestsSent(int playerId);


    }
}
