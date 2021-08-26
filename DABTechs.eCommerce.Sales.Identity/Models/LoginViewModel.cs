using System;
using System.Collections.Generic;
using System.Linq;

namespace DABTechs.eCommerce.Sales.Identity.Models
{
    public class LoginViewModel : LoginInputModel
    {
        public bool AllowRememberLogin { get; set; } = true;
    }
}