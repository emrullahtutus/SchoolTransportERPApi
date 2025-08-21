using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.User
{
    public class LoginRequest
    {
 
        public string UsernameOrEmail { get; set; }


        public string Password { get; set; }

        public int? AccessTokenLifeTime { get; set; }

        public LoginRequest()
        {
            AccessTokenLifeTime = 3600;
        }


    }

}
