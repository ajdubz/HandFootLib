using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models
{
    public class Player
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)] // Set the maximum length to 50 characters
        public string? NickName { get; set; }

        [StringLength(500)] // Set the maximum length to 500 characters
        public string? Email { get; set; }

        [StringLength(50)] // Set the maximum length to 500 characters
        public string? Password { get; set; }
        public int? Wins { get; set; } = 0;
        public int? Losses { get; set; } = 0;
        public int? GamesPlayed { get; set; } = 0;
        public int? TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Team? Team { get; set; }
        public List<int>? FriendIds { get; set; }

    }
}
