using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Models.DTOs.Player
{
    public class PlayerGetWithFriendsDTO
    {
        public int Id { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public TeamGetBasicDTO? Team { get; set; }
        public List<PlayerGetBasicDTO>? Friends { get; set; }

    }
}
