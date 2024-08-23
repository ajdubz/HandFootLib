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
    public class PlayerService(Data data) : IPlayerService
    {
        public void AddPlayer(PlayerSetAccountDTO playerSetAccountDTO)
        {
            try
            {
                var newPlayer = new Player
                {
                    NickName = playerSetAccountDTO.NickName,
                    Email = playerSetAccountDTO.Email,
                    Password = playerSetAccountDTO.Password,

                };

                data.Players.Add(newPlayer);
                data.SaveChanges();
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
                var player = data.Players.SingleOrDefault(p => p.Id == id);

                if (player == null)
                {
                    Console.WriteLine($"An error occurred: player with id {id} is null");
                    return;
                };

                data.PlayerFriends.RemoveRange(data.PlayerFriends.Where(pf => pf.PlayerId == id || pf.FriendId == id));

                data.Players.Remove(player);
                data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public void UpdatePlayerAccount(int playerId, PlayerSetAccountDTO playerSetAccountDTO)
        {
            try
            {
                var player = data.Players.SingleOrDefault(p => p.Id == playerId);

                if (player == null)
                {
                    Console.WriteLine($"An error occurred: player url with id {playerId} is null or invalid");
                    return;
                }

                player.NickName = playerSetAccountDTO.NickName;
                player.Email = playerSetAccountDTO.Email;
                player.Password = playerSetAccountDTO.Password;

                data.Players.Update(player);
                data.SaveChanges();
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

                data.PlayerFriends.Add(playerFriend);
                data.SaveChanges();
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
                data.PlayerFriends.RemoveRange(data.PlayerFriends.Where(pf => (pf.PlayerId == playerId && pf.FriendId == friendId) || (pf.FriendId == playerId && pf.PlayerId == friendId)));
                data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }





        public PlayerGetAccountDTO? GetPlayerAccount(int id)
        {
            try
            {
                var mainPlayers = GetPlayers();

                var getPlayers = mainPlayers.Select(x => new PlayerGetAccountDTO
                {
                    Id = x.Id,
                    NickName = x.NickName,
                    Email = x.Email,
                    Password = x.Password,

                });


                var player = getPlayers.ToList().SingleOrDefault(p => p.Id == id);

                return player;

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                throw;
            }
        }

        public PlayerGetFullDetailsDTO? GetPlayer(int id)
        {
            try
            {
                var getPlayers = GetPlayers();

                var allPlayers = getPlayers.Select(p => new PlayerGetFullDetailsDTO
                {
                    Id = p.Id,
                    NickName = p.NickName,
                    Friends = p.Friends,

                });

                var player = allPlayers.ToList().SingleOrDefault(p => p.Id == id);

                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }




        public IQueryable<PlayerGetAllDTO> GetPlayers()
        {
            try
            {
                var allPlayers = from p in data.Players
                                 select new PlayerGetAllDTO
                                 {
                                     Id = p.Id,
                                     NickName = p.NickName,
                                     Email = p.Email,
                                     Password = p.Password,
                                     Friends = GetFriends(p.Id).ToList(),
                                 };

                return allPlayers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public IQueryable<PlayerGetBasicDTO> GetPlayersBasic()
        {
            try
            {
                var getPlayers = GetPlayers();
                var allPlayers = getPlayers.Select(p => new PlayerGetBasicDTO
                                 {
                                     Id = p.Id,
                                     NickName = p.NickName,
                                 });

                return allPlayers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }




        private IQueryable<PlayerGetBasicDTO> GetFriends(int? pId)
        {
            try
            {
                var allFriends = from f in data.PlayerFriends
                                 join p in data.Players on f.FriendId equals p.Id
                                 where f.PlayerId == pId
                                 select new PlayerGetBasicDTO
                                 {
                                     Id = p.Id,
                                     NickName = p.NickName,
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
