using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandFootLib.Models
{
    public class GameTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? GameId { get; set; }

        [ForeignKey("GameId")]
        public Game? Game { get; set; }

        public int? TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Team? Team { get; set; }
        public int? Rank { get; set; }

    }
}
