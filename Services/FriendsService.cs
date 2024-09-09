using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;
using HandFootLib.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace HandFootLib.Services;

public class FriendsService : IFriendsService
{
    private readonly Data _data;
    private readonly ILogger<TeamService> _logger;

    public FriendsService(Data data, ILogger<TeamService> logger)
    {
        _data = data;
        _logger = logger;

    }

    public void AcceptFriendRequest(int playerId, int friendId)
    {
        try
        {
            if (playerId == 0)
            {
                throw new ArgumentException("PlayerId cannot be 0");
            }
            if (friendId == 0)
            {
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
        catch (ArgumentException ex)
        {
            // Handle the exception here
            // You can log the exception or perform any other necessary actions
            // For example:
            _logger.LogError(ex, "An ArgumentException occurred");
            throw; // Rethrow the exception to propagate it to the caller

        }
    }

    public void AddFriendRequest(int playerId, int friendId)
    {
        try
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
        catch (ArgumentException ex)
        {
            // Handle the exception here
            // You can log the exception or perform any other necessary actions
            // For example:
            _logger.LogError(ex, "An ArgumentException occurred");
            throw; // Rethrow the exception to propagate it to the caller
        }

    }

    public void DeclineFriendRequest(int playerId, int friendId)
    {

        try
        {
            if (playerId == 0)
            {
                throw new ArgumentException("PlayerId cannot be 0");
            }
            if (friendId == 0)
            {
                throw new ArgumentException("FriendId cannot be 0");
            }

            var existingRequest = _data.PlayerFriends.SingleOrDefault(pf => pf.PlayerId == playerId && pf.FriendId == friendId);

            if (existingRequest == null)
            {
                throw new ArgumentException("Friend request does not exist");
            }

            _data.PlayerFriends.Remove(existingRequest);
            _data.SaveChanges();
        }
        catch (ArgumentException ex)
        {
            // Handle the exception here
            // You can log the exception or perform any other necessary actions
            // For example:
            _logger.LogError(ex, "An ArgumentException occurred");
            throw; // Rethrow the exception to propagate it to the caller
        }


    }

    public IQueryable<PlayerGetAllDTO> GetFriendRequests(int playerId)
    {

        try
        {
            var friendSelect = from pf in _data.PlayerFriends
                join p in _data.Players on pf.PlayerId equals p.Id
                where pf.FriendId == playerId && pf.IsValidated == false
                select new PlayerGetAllDTO
                {
                    Id = pf.PlayerId,
                    NickName = p.NickName,
                    FullName = p.FullName,
                };

            return friendSelect;
        }
        catch (Exception ex)
        {
            // Handle the exception here
            // You can log the exception or perform any other necessary actions
            // For example:
            _logger.LogError(ex, "An error occurred while retrieving friend requests");
            throw; // Rethrow the exception to propagate it to the caller
        }

    }

    public IQueryable<PlayerGetAllDTO> GetFriendRequestsSent(int playerId)
    {
        try
        {
            var friendSelect = from pf in _data.PlayerFriends
                join p in _data.Players on pf.FriendId equals p.Id
                where pf.PlayerId == playerId && pf.IsValidated == false
                select new PlayerGetAllDTO
                {
                    Id = pf.FriendId,
                    NickName = p.NickName,
                    FullName = p.FullName,
                };

            return friendSelect;
        }
        catch (Exception ex)
        {
            // Handle the exception here
            // You can log the exception or perform any other necessary actions
            // For example:
            _logger.LogError(ex, "An error occurred while retrieving sent friend requests");
            throw; // Rethrow the exception to propagate it to the caller
        }


    }

    public IQueryable<PlayerGetAllDTO> GetFriends(int playerId)
    {

        try
        {
            var friendSelect = from pf in _data.PlayerFriends
                join p in _data.Players on pf.FriendId equals p.Id
                where (pf.PlayerId == playerId) && pf.IsValidated == true
                select new PlayerGetAllDTO
                {
                    Id = pf.FriendId,
                    NickName = p.NickName,
                    FullName = p.FullName,
                };

            return friendSelect;
        }
        catch (Exception ex)
        {
            // Handle the exception here
            // You can log the exception or perform any other necessary actions
            // For example:
            _logger.LogError(ex, "An error occurred while retrieving friends");
            throw; // Rethrow the exception to propagate it to the caller
        }





    }

}