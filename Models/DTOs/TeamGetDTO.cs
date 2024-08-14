using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs
{
    public  class TeamGetDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        //public List<PlayerGetBasicDTO>? Players { get; set; }
        //public int? PlayerId { get; set; }
        //public string? PlayerNickName { get; set; }
    }
}
