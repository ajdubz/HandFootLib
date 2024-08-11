using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly Data _data = new();

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

        public IQueryable<PlayerGetDTO> GetPlayer(int id)
        {
            var allPlayers = GetPlayers();

            var player = allPlayers.Where(p => p.Id == id);

            return player;
        }

        public IQueryable<PlayerGetDTO> GetPlayers()
        {

            var allPlayers = from p in _data.Players
                      select new PlayerGetDTO
                      {
                          Id = p.Id,
                          NickName = p.NickName,
                          Wins = p.Wins,
                          Losses = p.Losses,
                          GamesPlayed = p.GamesPlayed,
                          FriendIds = p.FriendIds //Should this be a list of PlayerGetDTO?
                      };

            return allPlayers;
        }

        //public List<PlayerGetDTO> GetPlayersForTeam(int teamId)
        //{
        //    var team = _data.Teams.Where(t => t.Id == teamId).FirstOrDefault();

        //    return team.Players.Select(player => new PlayerGetDTO()
        //        {
        //            Id = player.Id,
        //            NickName = player.NickName,
        //            Wins = player.Wins,
        //            Losses = player.Losses,
        //            GamesPlayed = player.GamesPlayed,
        //            FriendIds = player.FriendIds
        //        })
        //        .ToList();
        //}

        public void UpdatePlayer(Player player)
        {
            _data.Update(player);
            _data.SaveChanges();
        }
    }
}
