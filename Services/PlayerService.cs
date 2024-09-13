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
using Microsoft.Extensions.Logging;

namespace HandFootLib.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly Data _data;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(Data data, ILogger<PlayerService> logger)
        {
            _data = data;
            _logger = logger;
        }

        public PlayerSetAccountDTO AddPlayer(PlayerSetAccountDTO playerSetAccountDTO)
        {
            try
            {
                var newPlayer = new Player
                {
                    NickName = playerSetAccountDTO.NickName,
                    Email = playerSetAccountDTO.Email,
                    Password = playerSetAccountDTO.Password,
                    FullName = playerSetAccountDTO.FullName,
                };

                _data.Players.Add(newPlayer);
                _data.SaveChanges();

                return new PlayerSetAccountDTO
                {
                    Id = newPlayer.Id,
                    NickName = newPlayer.NickName,
                    Email = newPlayer.Email,
                    Password = newPlayer.Password,
                    FullName = newPlayer.FullName,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddPlayer");
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
                _logger.LogError(ex, "Error in RemovePlayer");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public void UpdatePlayerAccount(int playerId, PlayerSetAccountDTO playerSetAccountDTO)
        {
            try
            {
                var player = _data.Players.SingleOrDefault(p => p.Id == playerId);

                if (player == null)
                {
                    _logger.LogError("Error in UpdatePlayerAccount");
                    return;
                }

                player.NickName = playerSetAccountDTO.NickName;
                player.Email = playerSetAccountDTO.Email;
                player.Password = playerSetAccountDTO.Password;
                player.FullName = playerSetAccountDTO.FullName;

                _data.Players.Update(player);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdatePlayerAccount");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }


        public IQueryable<PlayerGetAllDTO> GetPlayers()
        {
            try
            {
                var allPlayers = from p in _data.Players
                                 select new PlayerGetAllDTO
                                 {
                                     Id = p.Id,
                                     NickName = p.NickName,
                                     Email = p.Email,
                                     Password = p.Password,
                                     FullName = p.FullName,
                                     Friends = GetFriends(p.Id).ToList(),
                                 };

                return allPlayers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPlayers");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        private IQueryable<PlayerGetBasicDTO> GetFriends(int? pId)
        {
            try
            {
                var allFriends = from f in _data.PlayerFriends
                                 join p in _data.Players on f.FriendId equals p.Id
                                 where f.PlayerId == pId
                                 select new PlayerGetBasicDTO
                                 {
                                     Id = p.Id,
                                     NickName = p.NickName,
                                     FullName = p.FullName,
                                 };

                return allFriends;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFriends");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

    }
}
