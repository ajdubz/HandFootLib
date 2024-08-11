using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs
{
    public class PlayerGetDTO
    {
        public int Id { get; set; }
        public string? NickName { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public int? GamesPlayed { get; set; }
        public List<int>? FriendIds { get; set; }

    }
}
