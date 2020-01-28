using System;
using SaleTaxApplication.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTaxApplication.Tax
{
    public abstract class TaxBase : ITaxBase
    {
        abstract public bool IsApplicable(Product item);
        abstract public decimal Rate { get; }
        public decimal Calculate(Product item)
        {
            if (IsApplicable(item))
            {
                //sales tax are that for a tax rate of n%, a shelf price of p contains (np/100)
                var tax = (item.Price * Rate) / 100;

                //The rounding rules: rounded up to the nearest 0.05
                tax = Math.Ceiling(tax / 0.05m) * 0.05m;

                return tax;
            }

            return 0;
        }
    }

}
