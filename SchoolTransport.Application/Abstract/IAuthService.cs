using SchoolTransport.Application.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
    }
}
