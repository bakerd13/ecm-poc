using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
        }
    }
}
