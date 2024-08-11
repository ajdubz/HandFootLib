using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs
{
    public  class TeamCreateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> PlayerIds { get; set; } = new List<int>();
    }
}
