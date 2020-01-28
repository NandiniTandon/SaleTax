using NUnit.Framework;
using SaleTaxApplication;
using SaleTaxApplication.Entities;
using SaleTaxApplication.Tax;
using System;
using System.Collections.Generic;

namespace UnitTestCaseSaleTaxApplication
{
    [TestFixture]
    public class TaxCalcuationFixture
    {
        protected ITotalTaxCalculator _calculator;
        protected BasketItem _basketItem;
        protected Basket _basket;
        protected Product _product;
        protected ShoppingStore _store;

        [SetUp]
        public virtual void Init()
        {
            _calculator = new TotalTaxCalculator();
            //Vanilla set up
            _product = new Product() { Category = ProductType.OTHER, Name = ".NET BOOK", IsImported = false, Price = 100.0m };
            _basketItem = new BasketItem() { Product = _product, Quantity = 1 };
            _basket = new Basket() { CartItems = new List<BasketItem>() };
            _basket.CartItems.Add(_basketItem);
            _store = new ShoppingStore();
        }


        [Test]
        public void ShouldCalculateBasicSalesTax()
        {
            var expected = 10.0m;
            _calculator.Calculate(_basket);
            var actual = _basket.TotalTax;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldCalculateBasicSalesTaxExempted()
        {
            _product.Category = ProductType.BOOK;
            var expected = 0.0m;
            _calculator.Calculate(_basket);
            var actual = _basket.TotalTax;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldCalculateImportDutyTax()
        {
            _product.IsImported = true;
            var expected = 15.0m;
            _calculator.Calculate(_basket);
            var actual = _basket.TotalTax;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldCalculateImportDutyTaxExempted()
        {
            _product.IsImported = true;
            _product.Category = ProductType.BOOK;
            var expected = 5.0m;
            _calculator.Calculate(_basket);
            var actual = _basket.TotalTax;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Input1()
        {
            Product bookProduct = new Product() { Category = ProductType.BOOK, Name = "book", IsImported = false, Price = 12.49m };
            Product musicProduct = new Product() { Category = ProductType.OTHER, Name = "music CD", IsImported = false, Price = 14.99m };
            Product chocolateProduct = new Product() { Category = ProductType.FOOD, Name = "chocolate bar", IsImported = false, Price = 0.85m };

            BasketItem basketItem1 = new BasketItem() { Product = bookProduct, Quantity = 1 };
            BasketItem basketItem2 = new BasketItem() { Product = musicProduct, Quantity = 1 };
            BasketItem basketItem3 = new BasketItem() { Product = chocolateProduct, Quantity = 1 };
            _basket = new Basket() { CartItems = new List<BasketItem>() };
            _basket.CartItems.Add(basketItem1);
            _basket.CartItems.Add(basketItem2);
            _basket.CartItems.Add(basketItem3);
            string expected = "1  book : 12.49" + Environment.NewLine
                            + "1  music CD : 16.49" + Environment.NewLine
                            + "1  chocolate bar : 0.85" + Environment.NewLine
                            + "Sales Taxes: 1.50 Total: 29.83";
            _calculator.Calculate(_basket);
            string output = _store.GetPrintText(_basket);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Input2()
        {
            Product importedOtherProduct = new Product() { Category = ProductType.OTHER, Name = "bottle of perfume", IsImported = true, Price = 47.50m };
            Product importedFoodProduct = new Product() { Category = ProductType.FOOD, Name = "box of chocolates", IsImported = true, Price = 10.00m };

            BasketItem basketItem1 = new BasketItem() { Product = importedFoodProduct, Quantity = 1 };
            BasketItem basketItem2 = new BasketItem() { Product = importedOtherProduct, Quantity = 1 };
            _basket = new Basket() { CartItems = new List<BasketItem>() };
            _basket.CartItems.Add(basketItem1);
            _basket.CartItems.Add(basketItem2);
            string expected = "1 imported box of chocolates : 10.50" + Environment.NewLine
                                + "1 imported bottle of perfume : 54.65" + Environment.NewLine
                                + "Sales Taxes: 7.65 Total: 65.15";
            _calculator.Calculate(_basket);
            string output = _store.GetPrintText(_basket);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Input3()
        {
            Product importedPerfumeProduct = new Product() { Category = ProductType.OTHER, Name = "bottle of perfume", IsImported = true, Price = 27.99m };
            Product perfumeProduct = new Product() { Category = ProductType.OTHER, Name = "bottle of perfume", IsImported = false, Price = 18.99m };
            Product medicalProduct = new Product() { Category = ProductType.MEDICAL, Name = "packet of headache pills", IsImported = false, Price = 9.75m };
            Product importedFoodProduct = new Product() { Category = ProductType.FOOD, Name = "box of chocolates", IsImported = true, Price = 11.25m };

            BasketItem basketItem1 = new BasketItem() { Product = importedPerfumeProduct, Quantity = 1 };
            BasketItem basketItem2 = new BasketItem() { Product = perfumeProduct, Quantity = 1 };
            BasketItem basketItem3 = new BasketItem() { Product = medicalProduct, Quantity = 1 };
            BasketItem basketItem4 = new BasketItem() { Product = importedFoodProduct, Quantity = 1 };
            _basket = new Basket() { CartItems = new List<BasketItem>() };
            _basket.CartItems.Add(basketItem1);
            _basket.CartItems.Add(basketItem2);
            _basket.CartItems.Add(basketItem3);
            _basket.CartItems.Add(basketItem4);
            string expected = "1 imported bottle of perfume : 32.19" + Environment.NewLine
                                +"1  bottle of perfume : 20.89" + Environment.NewLine
                                +"1  packet of headache pills : 9.75" + Environment.NewLine
                                +"1 imported box of chocolates : 11.85" + Environment.NewLine
                                +"Sales Taxes: 6.70 Total: 74.68";
            _calculator.Calculate(_basket);
            string output = _store.GetPrintText(_basket);
            Assert.AreEqual(expected, output);
        }

    }
}
