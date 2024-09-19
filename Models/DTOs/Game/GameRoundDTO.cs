using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Game
{
    public class GameRoundDTO
    {
        public int Id { get; set; }
        public GameTeamDTO? GameTeam { get; set; }
        public int? RoundNumber { get; set; }
        public int? HandScore { get; set; }
        public int? CleanBooks { get; set; }
        public int? DirtyBooks { get; set; }
        public int? RedThrees { get; set; }
        public int? PulledCorrect { get; set; }
        public bool? IsWinner { get; set; }
    }
}
