using SaleTaxApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTaxApplication.Entities
{
    public class BasketItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Tax { get; set; }

        public decimal Cost { get { return Quantity * (Tax + Product.Price); } }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} : {3:N2}", Quantity, Product.IsImported? "imported" :string.Empty, Product.Name, Cost);
        }
    }
}
