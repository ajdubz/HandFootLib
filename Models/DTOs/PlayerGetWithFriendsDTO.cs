using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs
{
    public class PlayerGetWithFriendsDTO
    {
        public int Id { get; set; }
        public string? NickName { get; set; }
        public TeamGetDTO? Team { get; set; }
        public List<PlayerGetBasicDTO>? Friends { get; set; }

    }
}
