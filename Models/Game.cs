using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HandFootLib.Services;

namespace HandFootLib.Models
{
    public class Game
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
