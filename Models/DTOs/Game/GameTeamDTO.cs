using HandFootLib.Models.DTOs.Team;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Game
{
    public class GameTeamDTO
    {
        public int Id { get; set; }
        public GameDTO? Game { get; set; }
        public TeamGetWithPlayerNamesDTO? Team { get; set; }
    }
}
