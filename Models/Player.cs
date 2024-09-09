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

        [StringLength(50)]
        public string? NickName { get; set; }

        [StringLength(500)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Password { get; set; }

        [StringLength(500)]
        public string? FullName { get; set; }



    }
}
