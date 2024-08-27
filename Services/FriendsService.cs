using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;
using HandFootLib.Services.Interfaces;

namespace HandFootLib.Services;

public class FriendsService : IFriendsService
{
    private readonly Data _data;

    public FriendsService(Data data) => _data = data;

    public void AcceptFriendRequest(int playerId, int friendId)
    {
        if (playerId == 0) {
            throw new ArgumentException("PlayerId cannot be 0");
        }
        if (friendId == 0) {
            throw new ArgumentException("FriendId cannot be 0");
        }

        var existingRequest = _data.PlayerFriends.SingleOrDefault(pf => pf.PlayerId == playerId && pf.FriendId == friendId);

        if (existingRequest == null)
        {
            throw new ArgumentException("Friend request does not exist");
        }

        existingRequest.IsValidated = true;

        var newFriend = new PlayerFriend
        {
            PlayerId = friendId,
            FriendId = playerId,
            IsValidated = true
        };

        _data.PlayerFriends.Update(existingRequest);
        _data.PlayerFriends.Add(newFriend);
        _data.SaveChanges();
    }

    public void AddFriendRequest(int playerId, int friendId)
    {
        if (playerId == 0)
        {
            throw new ArgumentException("PlayerId cannot be 0");
        }
        if (friendId == 0)
        {
            throw new ArgumentException("FriendId cannot be 0");
        }

        var newFriend = new PlayerFriend
        {
            PlayerId = playerId,
            FriendId = friendId,
            IsValidated = false
        };

        var existingRequest = _data.PlayerFriends.SingleOrDefault(pf => pf.PlayerId == playerId && pf.FriendId == friendId);

        if (existingRequest != null)
        {
            throw new ArgumentException("Friend request already exists");
        }


        _data.PlayerFriends.Add(newFriend);
        _data.SaveChanges();
    }

    public void DeclineFriendRequest(int playerId, int friendId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PlayerGetBasicDTO> GetFriendRequests(int playerId)
    {

        var friendSelect = from pf in _data.PlayerFriends
            join p in _data.Players on pf.PlayerId equals p.Id
            where pf.FriendId == playerId && pf.IsValidated == false
            select new PlayerGetBasicDTO
            {
                Id = pf.PlayerId,
                NickName = p.NickName
            };

        return friendSelect;
    }

    public IQueryable<PlayerGetBasicDTO> GetFriendRequestsSent(int playerId)
    {
        var friendSelect = from pf in _data.PlayerFriends
            join p in _data.Players on pf.FriendId equals p.Id
            where pf.PlayerId == playerId && pf.IsValidated == false
            select new PlayerGetBasicDTO
            {
                Id = pf.FriendId,
                NickName = p.NickName
            };

        return friendSelect;
    }

    public IQueryable<PlayerGetBasicDTO> GetFriends(int playerId)
    {
        throw new NotImplementedException();
    }

    public void RemoveFriendRequest(int playerId, int friendId)
    {
        throw new NotImplementedException();
    }
}