using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMigrationsBack.Interfaces
{
    public interface IJWTokenService
    {
       Task<string> GenerateJwtToken(string email);
    }
}