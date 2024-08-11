using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)] // Set the maximum length to 50 characters
        public string? Name { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<GameTeam> Games { get; set; } = new List<GameTeam>();

    }
}
