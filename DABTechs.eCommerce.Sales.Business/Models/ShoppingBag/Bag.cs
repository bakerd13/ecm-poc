using DABTechs.eCommerce.Sales.Business.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace DABTechs.eCommerce.Sales.Business.Models.ShoppingBag
{
    public class Bag
    {
        public string Id { get; set; }

        public List<ProductItemCollection> Items { get; set; }
    }
}
