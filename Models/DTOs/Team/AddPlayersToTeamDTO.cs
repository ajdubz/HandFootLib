using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Team
{
    public class AddPlayersToTeamDTO
    {
        public int? PlayerId1 { get; set; }
        public int? PlayerId2 { get; set; }
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }

    }
}
