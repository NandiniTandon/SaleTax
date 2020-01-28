using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTaxApplication.Entities
{
    public class Basket
    {
        public IList<BasketItem> CartItems { get; set; }

        public decimal TotalTax { get { return CartItems.Sum(x => x.Tax); } }

        public decimal TotalCost { get { return CartItems.Sum(x => x.Cost); } }
    }
}
