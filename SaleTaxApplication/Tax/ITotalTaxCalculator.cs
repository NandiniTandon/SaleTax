using SaleTaxApplication.Entities;

namespace SaleTaxApplication.Tax
{
    public interface ITotalTaxCalculator
    {
        void Calculate(Basket basket);
    }
}