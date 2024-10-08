﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models
{
    public class GameRound
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? GameTeamId { get; set; }

        [ForeignKey("GameTeamId")]
        public GameTeam? GameTeam { get; set; }
        public int? RoundNumber { get; set; }
        public int? HandScore { get; set; }
        public int? CleanBooks { get; set; }
        public int? DirtyBooks { get; set; }
        public int? RedThrees { get; set; }
        public int? PulledCorrect { get; set; }
        public bool? IsWinner { get; set; }

    }
}
