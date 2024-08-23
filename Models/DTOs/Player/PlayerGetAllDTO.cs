using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Player
{
    public class PlayerGetAllDTO
    {
        public int? Id { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<GameTeam>? GameTeams { get; set; }
        public List<PlayerGetBasicDTO>? Friends { get; set; }

    }
}
