using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Models.DTOs.Player
{
    public class PlayerGetFullDetailsDTO
    {
        public int? Id { get; set; }
        public string? NickName { get; set; }
        public List<GameTeam>? GameTeams { get; set; }
        public List<PlayerGetBasicDTO>? Friends { get; set; }

    }
}
