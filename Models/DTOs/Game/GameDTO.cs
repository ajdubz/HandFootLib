using HandFootLib.Models.DTOs.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Game
{
    public class GameDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public RulesGetDTO? Rules { get; set; }
    
    }
}
