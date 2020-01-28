using SaleTaxApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleTaxApplication.Tax
{
    public interface ITaxBase
    {
        decimal Calculate(Product item);
    }
}
