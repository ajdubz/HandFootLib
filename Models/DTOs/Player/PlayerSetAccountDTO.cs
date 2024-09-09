using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandFootLib.Models.DTOs.Team;

namespace HandFootLib.Models.DTOs.Player
{
    public class PlayerSetAccountDTO
    {
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }


    }
}
