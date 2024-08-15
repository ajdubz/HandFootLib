using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Team
{
    public class TeamCreateDTO
    {
        public string? Name { get; set; } = "No Team";
        public List<int> PlayerIds { get; set; } = [];
    }
}
