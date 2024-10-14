using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Auth.Common
{
    public record AuthResult(string jwtToken, string refreshToken);
}
