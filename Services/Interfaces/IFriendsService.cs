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

        void AcceptFriendRequest(int playerId, int friendId);

        void DeclineFriendRequest(int playerId, int friendId);

        IQueryable<PlayerGetAllDTO> GetFriends(int playerId);

        IQueryable<PlayerGetAllDTO> GetFriendRequests(int playerId);

        IQueryable<PlayerGetAllDTO> GetFriendRequestsSent(int playerId);


    }
}
