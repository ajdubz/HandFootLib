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
        public List<GameTeam> Teams { get; set; } = new List<GameTeam>();
        public DateTime? Date { get; set; }
    }
}
