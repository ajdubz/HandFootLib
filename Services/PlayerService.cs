using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        //Can this use the GetPlayer method?
        public void DeletePlayer(int id)
        {
            var players = _data.Players.Where(p => p.Id == id);

            var player = players.FirstOrDefault();

            _data.Players.Remove(player);
            _data.SaveChanges();
        }

        public PlayerGetBasicDTO? GetPlayer(int id)
        {
            var allPlayers = GetPlayers();

            var player = allPlayers.SingleOrDefault(p => p.Id == id);

            return player;
        }

        private IQueryable<PlayerGetBasicDTO> GetFriends(Player player)
        {
            var friendIds = player.FriendIds;

            if (friendIds == null) {  return new List<PlayerGetBasicDTO>().AsQueryable(); }

            var friends = _data.Players.Where(p => friendIds.Contains(p.Id))
                                      .Select(p => new PlayerGetBasicDTO
                                      {
                                          Id = p.Id,
                                          NickName = p.NickName,
                                          Team = p.Team
                                      });

            return friends;
        }

        public IQueryable<PlayerGetBasicDTO> GetPlayers()
        {

            // ReSharper disable once EntityFramework.NPlusOne.IncompleteDataQuery
            var all = _data.Players.ToList();

            var allPlayers = all.Select( p => new PlayerGetBasicDTO
                      {
                          Id = p.Id,
                          NickName = p.NickName,
                          Team = p.Team,
                          Friends = GetFriends(p).ToList()

                      });

            return allPlayers.AsQueryable();
        }

        public void UpdatePlayer(Player player)
        {

            _data.Update(player);
            _data.SaveChanges();
        }
    }
}
