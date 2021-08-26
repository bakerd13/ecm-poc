using System;
using System.Collections.Generic;
using System.Linq;
using DABTechs.eCommerce.Sales.Providers.Azure.Interfaces;

namespace DABTechs.eCommerce.Sales.Providers.Azure.Models
{
    public class Item : IItem
    {
        public List<ItemValue> Values { get; set; }

        public ItemValue this[string name]
        {
            get
            {
                var itemValue = this.Values.FirstOrDefault(v => string.Equals(v.Name, name, StringComparison.InvariantCultureIgnoreCase));
                return itemValue;
            }
        }

        public Item()
        {
            Values = new List<ItemValue>();
        }

        public bool HasValue(string name)
        {
            return Values.Any(v => string.Equals(v.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}