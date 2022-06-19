using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge.Authentication
{
    public class UserModel
    {
        public string Username { get; set; }        

        public string Password { get; set; }

        public string UserGroup { get; set; }
    }
}
