using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest.Services.Models
{
    public class AccountInfo
    {
        public string accountId { get; set; }

        public string userEmail { get; set; }
        public DateTime loginTime { get; set; }
    }
}
