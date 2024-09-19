using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Rules
{
    public class RulesGetDTO
    {
        public DateTime? Date { get; set; } = DateTime.Now;
        public int? CleanBookScore { get; set; } = 500;
        public int? DirtyBookScore { get; set; } = 300;
        public int? RedThreeScore { get; set; } = 300;
        public int? PulledScore { get; set; } = 100;
        public int? WinnerScore { get; set; } = 100;
        public int? CardsToDraw { get; set; } = 3;
        public int? CardsToStart { get; set; } = 26;
        public int[]? RoundThresholds { get; set; } = { 60, 90, 120, 150 };
    }
}
