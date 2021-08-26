using System.Collections.Generic;
using System.Linq;

namespace DABTechs.eCommerce.Sales.Providers.Azure.Models
{
    public class ItemValue
    {
        public string Name { get; set; }

        public List<string> Values { get; set; }

        public ItemValue()
        {
            Values = new List<string>();
        }

        public override string ToString()
        {
            return Name;
        }

        public string this[int index]
        {
            get
            {
                if (index > Values.Count - 1) { return null; }
                string itemValue = Values[index];
                return itemValue;
            }
        }

        public string Value
        {
            get
            {
                return Values.FirstOrDefault();
            }
        }

        public IList<string> Options
        {
            get
            {
                return Values;
            }
        }

        public int Count
        {
            get
            {
                return Values.Count;
            }
        }
    }
}