using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManager.API.Token
{
    public interface ITokenGenerator
    {
        string GenerateToken();
    }
}