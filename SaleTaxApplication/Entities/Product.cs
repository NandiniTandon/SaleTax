using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTaxApplication.Entities
{
    /// <summary>
    /// Product entity to contain product information
    /// </summary>
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsImported { get; set; }
        public ProductType Category { get; set; }
        public override string ToString()
        {
            return string.Format("{0} at {1}", Name, Price);
        }
    }
}
