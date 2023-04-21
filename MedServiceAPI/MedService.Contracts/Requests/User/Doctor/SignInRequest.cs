using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.Contracts.Requests.User.Doctor
{
    public class SignInRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
