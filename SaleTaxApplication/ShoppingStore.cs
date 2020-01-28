using SaleTaxApplication.Entities;
using SaleTaxApplication.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTaxApplication
{
    public class ShoppingStore
    {
        private Basket _basket;

        public ShoppingStore()
        {
            _basket = new Basket() { CartItems = new List<BasketItem>() };
        }

        public void RetrieveOrderAndPlaceInCart(string name, ProductType type, decimal price, bool imported, int quantity)
        {
            Product product = new Product() { Name = name, Category = type, Price = price, IsImported = imported };
            BasketItem _basketItem = new BasketItem() { Product = product, Quantity = quantity };
            _basket.CartItems.Add(_basketItem);
        }

        public void GetSalesOrder()
        {
            do
            {
                string name = GetProductName();
                ProductType type = GetProductCategory();
                decimal price = GetProductPrice();
                bool imported = IsProductImported();
                int quantity = GetQuantity();
                RetrieveOrderAndPlaceInCart(name, type, price, imported, quantity);
            }
            while (HaveMoreProduct());
        }

        public void CheckOut()
        {
            ITotalTaxCalculator taxCalculator = new TotalTaxCalculator();
            taxCalculator.Calculate(_basket);
            Print(_basket);
        }

        public String GetProductName()
        {
            Console.WriteLine("Enter the product name:\n");
            return Console.ReadLine();
        }

        public ProductType GetProductCategory()
        {
            Console.WriteLine("Enter the product Category from following: FOOD, MEDICAL, BOOK, OTHER\n");
            string type = Console.ReadLine();

            ProductType productType;
            while (!(Enum.TryParse(type.ToUpper(), out productType)))
            {
                Console.WriteLine("Invalid category. Enter the valid product category");
            }

            return productType;
        }
        public decimal GetProductPrice()
        {
            Console.WriteLine("Enter the product price:\n");
            var input = Console.ReadLine();
            decimal val;
            while (!(decimal.TryParse(input, out val)))
            {
                Console.WriteLine("Invalid price. Enter a number");
            }

            return val;
        }

        public bool IsProductImported()
        {
            Console.WriteLine("Is product imported or not?(Y/N)\n");
            var input = Console.ReadLine();
            bool isValid = false;
            while (!isValid)
            {
                if (input == "Y" || input == "y")
                    isValid = true;
                else if (input == "N" || input == "n")
                    isValid = true;
                else
                    Console.WriteLine("Invalid input. Enter (Y/N)");
            }

            if (input == "Y")
                return true;
            else
                return false;
        }

        public int GetQuantity()
        {
            Console.WriteLine("Enter the quantity:\n");
            var input = Console.ReadLine();
            int intVal;
            while (!(int.TryParse(input, out intVal)))
            {
                Console.WriteLine("Invalid input. Enter a integer");
            }
            return intVal;
        }

        public bool HaveMoreProduct()
        {
            Console.WriteLine("Do you want to add another Product?(Y/N)");

            var input = Console.ReadLine();
            while (!(input == "Y" ||input == "y" || input == "N"|| input == "n"))
            {
                Console.WriteLine("Invalid input. Enter (Y/N)");
            }

            bool addAnotherProduct = ParseBoolean(Convert.ToChar(input));
            return addAnotherProduct;
        }

        public static bool ParseBoolean(char value)
        {
            bool flag = true;
            bool boolValue = false;

            while (flag)
            {
                //parses 'Y' into 'true'
                if (value == 'Y' || value == 'y')
                {
                    boolValue = true;
                    flag = false;
                }

                //parses 'N' into 'false'
                else if (value == 'N' || value == 'n')
                {
                    boolValue = false;
                    flag = false;
                }

                //validates user input
                else
                {
                    Console.WriteLine("Invalid input. Enter (Y/N)");
                }
            }

            return boolValue;
        }

        public string GetPrintText(Basket _basket)
        {
            StringBuilder builder = new StringBuilder();

            //printe items => 1 chocolate bar: 0.85
            foreach (var cartItem in _basket.CartItems)
            {
                builder.AppendLine(cartItem.ToString());
                // Console.WriteLine(cartItem.ToString());
            }

            //print Sales => Taxes: 1.50 Total: 29.83
            builder.AppendFormat("Sales Taxes: {0:N2} Total: {1:N2}", _basket.TotalTax, _basket.TotalCost);

            return builder.ToString();
        }
        public void Print(Basket _basket)
        {
            Console.Write(GetPrintText(_basket));

        }
    }
}
