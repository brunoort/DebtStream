﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest.Services.Models
{
    public class UpdateEmailViewModel
    {
        public string NewEmail { get; set; }
        public string ConfirmEmail { get; set; }
        public string CurrentPassword { get; set; }
    }

}
