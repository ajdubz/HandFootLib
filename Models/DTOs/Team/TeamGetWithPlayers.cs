using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandFootLib.Models.DTOs.Player;

namespace HandFootLib.Models.DTOs.Team
{
    public class TeamGetWithPlayers
    {
        public int? Id { get; set; } = 0;
        public string? Name { get; set; } = "No Team";
        public List<PlayerGetBasicDTO> Players { get; set; } = [];
    }
}
