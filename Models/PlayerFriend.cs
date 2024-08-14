using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models
{
    public class PlayerFriend
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int FriendId { get; set; }

        //[ForeignKey("PlayerId")]
        //public Player Player { get; set; }
        //[ForeignKey("FriendId")]
        //public Player Friend { get; set; }
    }
}
