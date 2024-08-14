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

namespace HandFootLib.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly Data _data;

        public PlayerService(Data data) { _data = data; }

        public void AddPlayer(Player player)
        {
            _data.Add(player);
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
            var playerFriend = _data.PlayerFriends.SingleOrDefault(pf => pf.PlayerId == playerId && pf.FriendId == friendId);

            _data.PlayerFriends.Remove(playerFriend);
            _data.SaveChanges();
        }

        public void RemovePlayer(int id)
        {

            var player = _data.Players.SingleOrDefault(p => p.Id == id);

            _data.Players.Remove(player);
            _data.SaveChanges();
        }

        public PlayerGetWithFriendsDTO? GetPlayer(int id)
        {
            var allPlayers = GetPlayers();

            var player = allPlayers.SingleOrDefault(p => p.Id == id);

            return player;
        }

        private IQueryable<PlayerGetBasicDTO> GetFriends(int pId)
        {

            var tryAgain = from f in _data.PlayerFriends
                           join p in _data.Players
                           on f.FriendId equals p.Id
                           where f.PlayerId == pId
                           select new PlayerGetBasicDTO
                           {
                               Id = p.Id,
                               NickName = p.NickName,
                               TeamDTO = new TeamGetDTO { Id = p.Team.Id, Name = p.Team.Name ?? "No Team Found friends" },
                           };

            return tryAgain;

            //var playersFriends = _data.PlayerFriends.Where(pf => pf.PlayerId == pId).ToList();

            //var tempAllFriends = new List<PlayerGetBasicDTO>();

            //foreach (var pf in playersFriends)
            //{
            //    var friend = GetPlayer(pf.FriendId);

            //    if (friend != null) tempAllFriends.Add(friend);
            //}

            //return tempAllFriends.AsQueryable();



            //var allFriends = from f in _data.PlayerFriends
            //                 join p in _data.Players
            //                 on f.FriendId equals p.Id
            //                 where f.PlayerId == pId
            //                 select new PlayerGetBasicDTO
            //                 {
            //                     Id = p.Id,
            //                     NickName = p.NickName,
            //                 };

            //return allFriends;
        }

        public IQueryable<PlayerGetWithFriendsDTO> GetPlayers()
        {

            var all = (from p in _data.Players select new { p }).ToList();



            var allPlayers = from p2 in all
                             select new PlayerGetWithFriendsDTO
                             {
                                 Id = p2.p.Id,
                                 NickName = p2.p.NickName,
                                 Friends = GetFriends(p2.p.Id).ToList(),

                             };

            return allPlayers.AsQueryable();

        }

        public void UpdatePlayer(PlayerUpdateDTO playerUpdateDTO)
        {
            var player = _data.Players.SingleOrDefault(p => p.Id == playerUpdateDTO.Id);

            player.NickName = playerUpdateDTO.NickName;
            player.Email = playerUpdateDTO.Email;


            _data.Update(player);
            _data.SaveChanges();
        }

        public PlayerGetWithFriendsDTO GetPlayerWithFriends(int id)
        {
           
            var allPlayers = GetPlayersWithFriends();

            var player = allPlayers.SingleOrDefault(p => p.Id == id);

            return player;
        }

        public IQueryable<PlayerGetWithFriendsDTO> GetPlayersWithFriends()
        {

            //var allPlayers = GetPlayers().ToList();





            //var allPlayersWithFriends = from p in allPlayers
            //                            select new PlayerGetWithFriendsDTO
            //                            {
            //                                Id = p.Id,
            //                                NickName = p.NickName,
            //                                Friends = GetFriends(p.Id).ToList(),
            //                                TeamDTO = new TeamGetDTO { Id = p.Team.Id, Name = p.Team.Name ?? "No Team Found friends" }

            //                            };

            //return allPlayersWithFriends;



            var all = (from p in _data.Players
                       join pf in _data.PlayerFriends
                       on p.Id equals pf.PlayerId
                       select new { p }
                       )
                       .ToList();



            var allPlayers = from p2 in all
                             select new PlayerGetWithFriendsDTO
                             {
                                 Id = p2.p.Id,
                                 NickName = p2.p.NickName,
                                 Friends = GetFriends(p2.p.Id).ToList(),

                             };

            return allPlayers.AsQueryable();

        }


    }
}
