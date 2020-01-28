using SaleTaxApplication.Entities;
using System.Linq;

namespace SaleTaxApplication.Tax
{
    public class SalesTax : TaxBase
    {
        private ProductType[] _taxExcemptions = new[] { ProductType.FOOD, ProductType.MEDICAL, ProductType.BOOK };
        public override bool IsApplicable(Product item)
        {
            return !(_taxExcemptions.Contains(item.Category));
        }
        public override decimal Rate { get { return 10.00M; } }
    }

}
