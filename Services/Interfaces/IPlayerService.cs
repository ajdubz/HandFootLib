using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Services.Interfaces
{
    public interface IPlayerService
    {
        public void AddPlayer(Player player);

        public void DeletePlayer(int id);

        public IQueryable<PlayerGetDTO> GetPlayer(int id);

        public IQueryable<PlayerGetDTO> GetPlayers();

        //public List<PlayerGetDTO> GetPlayersForTeam(int teamId);

        public void UpdatePlayer(Player player);
    }
}
