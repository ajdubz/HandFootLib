﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs.Team
{
    public class TeamGetBasicDTO
    {
        public int? Id { get; set; } = 0;
        public string? Name { get; set; } = "No Team";
    }
}