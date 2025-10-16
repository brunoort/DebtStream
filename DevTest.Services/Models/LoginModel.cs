using DevTest.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest.Services.Models
{
    public class LoginModel : LoginViewModel
    {
        public string AccountId { get; set; }

        public bool isSuccessful { get; set; }
    }
}
