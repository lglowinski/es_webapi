using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Authorization
{
    public interface IJWTManager
    {
        string GenerateToken(string id, string username, int expireMinutes = 20);
    }
}
