using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Models.DTOs.Player
{
    public class PlayerGetBasicDTO
    {
        public int Id { get; set; }
        public string? NickName { get; set; }
        public TeamGetBasicDTO? Team { get; set; }

    }
}
