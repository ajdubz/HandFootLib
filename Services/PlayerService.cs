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
            try
            {
                _data.Players.Add(player);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public void RemovePlayer(int id)
        {
            try
            {
                var player = _data.Players.SingleOrDefault(p => p.Id == id);

                if (player == null)
                {
                    Console.WriteLine($"An error occurred: player with id {id} is null");
                    return;
                };

                _data.PlayerFriends.RemoveRange(_data.PlayerFriends.Where(pf => pf.PlayerId == id || pf.FriendId == id));

                _data.Players.Remove(player);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }


        public void UpdatePlayer(PlayerUpdateDTO playerUpdateDTO)
        {
            try
            {
                var player = _data.Players.SingleOrDefault(p => p.Id == playerUpdateDTO.Id);

                if (player == null)
                {
                    Console.WriteLine($"An error occurred: player with id {playerUpdateDTO.Id} is null");
                    return;
                }

                player.NickName = playerUpdateDTO.NickName;
                player.Email = playerUpdateDTO.Email;

                _data.Update(player);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }



        public void AddFriend(int playerId, int friendId)
        {
            try
            {
                var playerFriend = new PlayerFriend
                {
                    PlayerId = playerId,
                    FriendId = friendId
                };

                _data.PlayerFriends.Add(playerFriend);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public void RemoveFriend(int playerId, int friendId)
        {
            try
            {
                var playerFriend = _data.PlayerFriends.SingleOrDefault(pf => pf.PlayerId == playerId && pf.FriendId == friendId) ??
                                   _data.PlayerFriends.SingleOrDefault(pf => pf.PlayerId == friendId && pf.FriendId == playerId);

                if (playerFriend == null) return;

                _data.PlayerFriends.Remove(playerFriend);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }



        public PlayerGetWithFriendsDTO? GetPlayer(int id)
        {
            try
            {
                var allPlayers = GetPlayers();

                var player = allPlayers.SingleOrDefault(p => p.Id == id);

                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public IQueryable<PlayerGetWithFriendsDTO> GetPlayers()
        {
            try
            {
                var all = (from p in _data.Players select new { p }).ToList();
                var allTeams = (from t in _data.Teams select new { t });

                var allPlayers = from p2 in all
                                 join t2 in _data.Teams on p2.p.TeamId equals t2.Id into teams
                                 from t2 in teams.DefaultIfEmpty()
                                 select new PlayerGetWithFriendsDTO
                                 {
                                     Id = p2.p.Id,
                                     NickName = p2.p.NickName,
                                     Email = p2.p.Email,
                                     Team = t2 != null ? new TeamGetBasicDTO { Id = t2.Id, Name = t2.Name } : new TeamGetBasicDTO { Id = 0, Name = "No Team" },
                                     Friends = GetFriends(p2.p.Id).ToList(),
                                 };

                return allPlayers.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }



        private IQueryable<PlayerGetBasicDTO> GetFriends(int pId)
        {
            try
            {
                var allTeams = (from t in _data.Teams select new { t });

                var allFriends = from f in _data.PlayerFriends
                                 join p in _data.Players on f.FriendId equals p.Id
                                 join t2 in _data.Teams on p.TeamId equals t2.Id into teams
                                 from t2 in teams.DefaultIfEmpty()
                                 where f.PlayerId == pId
                                 select new PlayerGetBasicDTO
                                 {
                                     Id = p.Id,
                                     NickName = p.NickName,
                                     Team = t2 != null ? new TeamGetBasicDTO { Id = t2.Id, Name = t2.Name } : new TeamGetBasicDTO { Id = 0, Name = "No Team" },
                                 };

                return allFriends;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

    }
}
