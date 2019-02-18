using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Models
{
    public class UserRequestModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
