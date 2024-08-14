﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandFootLib.Models.DTOs
{
    public class PlayerUpdateDTO
    {
        public int Id { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public int? TeamId { get; set; }

    }
}
