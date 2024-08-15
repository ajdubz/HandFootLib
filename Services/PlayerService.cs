using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using HandFootLib.Models.DTOs.Player;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly Data _data;

        public PlayerService(Data data) { _data = data; }

        public void AddPlayer(Player player)
        {
            _data.Players.Add(player);
            _data.SaveChanges();
        }

        public void RemovePlayer(int id)
        {
            var player = _data.Players.SingleOrDefault(p => p.Id == id);

            if (player == null) return;

            _data.PlayerFriends.RemoveRange(_data.PlayerFriends.Where(pf => pf.PlayerId == id || pf.FriendId == id));

            _data.Players.Remove(player);
            _data.SaveChanges();
        }

        public void UpdatePlayer(PlayerUpdateDTO playerUpdateDTO)
        {
            var player = _data.Players.SingleOrDefault(p => p.Id == playerUpdateDTO.Id);

            player.NickName = playerUpdateDTO.NickName;
            player.Email = playerUpdateDTO.Email;

            _data.Update(player);
            _data.SaveChanges();
        }

        public void AddFriend(int playerId, int friendId)
        {
            var playerFriend = new PlayerFriend
            {
                PlayerId = playerId,
                FriendId = friendId
            };

            _data.PlayerFriends.Add(playerFriend);
            _data.SaveChanges();
        }

        public void RemoveFriend(int playerId, int friendId)
        {
            var playerFriend = _data.PlayerFriends.SingleOrDefault(pf => pf.PlayerId == playerId && pf.FriendId == friendId) ??
                               _data.PlayerFriends.SingleOrDefault(pf => pf.PlayerId == friendId && pf.FriendId == playerId);

            if (playerFriend == null) return;

            _data.PlayerFriends.Remove(playerFriend);
            _data.SaveChanges();
        }

        public PlayerGetWithFriendsDTO? GetPlayer(int id)
        {
            var allPlayers = GetPlayers();

            var player = allPlayers.SingleOrDefault(p => p.Id == id);

            return player;
        }

        public PlayerGetWithFriendsDTO GetPlayerWithFriends(int id)
        {
            var allPlayers = GetPlayersWithFriends();

            var player = allPlayers.SingleOrDefault(p => p.Id == id);

            return player;
        }

        public IQueryable<PlayerGetWithFriendsDTO> GetPlayers()
        {
            var all = (from p in _data.Players select new { p }).ToList();

            var allPlayers = from p2 in all
                             select new PlayerGetWithFriendsDTO
                             {
                                 Id = p2.p.Id,
                                 NickName = p2.p.NickName,
                                 Team = new TeamGetBasicDTO { Id = p2.p.TeamId ?? 0, Name = p2.p.Team?.Name ?? "No Team" },
                                 Friends = GetFriends(p2.p.Id).ToList(),
                             };

            return allPlayers.AsQueryable();
        }

        public IQueryable<PlayerGetWithFriendsDTO> GetPlayersWithFriends()
        {
            var all = (from p in _data.Players
                       join pf in _data.PlayerFriends on p.Id equals pf.PlayerId
                       select new { p }).ToList();

            var allPlayers = from p2 in all
                             select new PlayerGetWithFriendsDTO
                             {
                                 Id = p2.p.Id,
                                 NickName = p2.p.NickName,
                                 Friends = GetFriends(p2.p.Id).ToList(),
                             };

            return allPlayers.AsQueryable();
        }

        private IQueryable<PlayerGetBasicDTO> GetFriends(int pId)
        {
            var allFriends = from f in _data.PlayerFriends
                             join p in _data.Players on f.FriendId equals p.Id
                             where f.PlayerId == pId
                             select new PlayerGetBasicDTO
                             {
                                 NickName = p.NickName,
                                 Team = new TeamGetBasicDTO { Id = p.TeamId ?? 0, Name = p.Team.Name ?? "No Team" },
                             };

            return allFriends;
        }
    }
}
