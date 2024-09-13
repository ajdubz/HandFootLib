using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Team
{
    public class GetTeamByPlayerIds
    {
        public int PlayerId1 { get; set; }
        public int? PlayerId2 { get; set; }
    }
}
