using SaleTaxApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTaxApplication.Tax
{
    public class TotalTaxCalculator : ITotalTaxCalculator
    {
        private IEnumerable<ITaxBase> _Taxes ;

        public TotalTaxCalculator()
        {
            _Taxes = ApplicableTaxes.GetApplicableTaxes();
        }
        public void Calculate(Basket basket)
        {
            foreach (var cartItem in basket.CartItems)
            {
                cartItem.Tax = _Taxes.Sum(x => x.Calculate(cartItem.Product));
            }
        }
    }
}
