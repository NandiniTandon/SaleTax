using System;
using System.Collections.Generic;
using System.Text;

namespace SaleTaxApplication.Tax
{
    public static class ApplicableTaxes
    {
        public static IEnumerable<ITaxBase> GetApplicableTaxes()
        {
            yield return new SalesTax();
            yield return new ImportedDutyTax();
        }
    }
}
