using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Models
{
    public class UserResponseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsSuccessful { get; set; }
    }
}
