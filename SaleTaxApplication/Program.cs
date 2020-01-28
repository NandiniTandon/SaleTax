using System;

namespace SaleTaxApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingStore store = new ShoppingStore();
            store.GetSalesOrder();
            store.CheckOut();

            Console.ReadKey();
        }
    }
}
