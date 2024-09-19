using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models
{
    public class Rules
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
