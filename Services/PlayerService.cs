﻿using HandFootLib.Models;
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
    public class PlayerService(Data data, ILogger<PlayerService> _logger) : IPlayerService
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
                _logger.LogError(ex, "Error in AddPlayer");
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
                _logger.LogError(ex, "Error in RemovePlayer");
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
                _logger.LogError(ex, "Error in UpdatePlayerAccount");
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
                _logger.LogError(ex, "Error in GetPlayers");
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
                _logger.LogError(ex, "Error in GetFriends");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

    }
}
