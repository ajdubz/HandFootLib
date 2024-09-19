using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Rules
{
    public class RulesSetDTO
    {
        public int? Id { get; set; }
        public DateTime? Date { get; set; }
        public int? CleanBookScore { get; set; }
        public int? DirtyBookScore { get; set; }
        public int? RedThreeScore { get; set; }
        public int? PulledScore { get; set; }
        public int? WinnerScore { get; set; }
        public int? CardsToDraw { get; set; }
        public int? CardsToStart { get; set; }
        public int[]? RoundThresholds { get; set; }
    }
}
