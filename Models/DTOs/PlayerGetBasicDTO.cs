using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs
{
    public class PlayerGetBasicDTO
    {
        public int Id { get; set; }
        public string? NickName { get; set; }
        public Team? Team { get; set; }
        public List<PlayerGetBasicDTO>? Friends { get; set; }

    }
}
