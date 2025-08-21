using SchoolTransport.Application.DTOs.Token;
using SchoolTransport.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int second, User user);

    }


}
