using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ShoppingCart
{
    public class Tests
    {
        private List<ProductRepository> PR = new List<ProductRepository>();

        private List<ShoppingCartItem> SC = new List<ShoppingCartItem>();


        [SetUp]
        public void Setup()
        {
            PR.Add(new ProductRepository(1, "Orange juice", 10, 1));
            PR.Add(new ProductRepository(2, "Apple juice", 12, 3));
            PR.Add(new ProductRepository(3, "Pineapple juice", 14, 5));
            PR.Add(new ProductRepository(4, "Banana juice", 16, 7));
            PR.Add(new ProductRepository(5, "Cherry juice", 18, 9));
        }

        public void AddProduct(int productid, int quantity)
        {
            if (PR.Count(x => x.Id == productid) == 0)
            {
                throw new CannotAddProductNotBelongingToRepository();
            }
            else
            {
                if (quantity <= 0)
                {
                    throw new QuantityCannotBeNegativeException();
                }
                if (PR.First(x => x.Id == productid).AvailibilityQuantity < quantity)
                {
                    throw new HigherQuantityAddedThanAvailableException();
                }
            }

        }

        public void AddProductTest(int productid, int quantity)
        {
            if (PR.Count(x => x.Id == productid) == 0)
            {
                throw new CannotAddProductNotBelongingToRepository();
            }
            else
            {
                if (quantity <= 0)
                {
                    throw new QuantityCannotBeNegativeException();
                }
                if (PR.First(x => x.Id == productid).AvailibilityQuantity < quantity)
                {
                    throw new HigherQuantityAddedThanAvailableException();
                }
            }
            SC.Add(new ShoppingCartItem(productid, quantity, PR.First(x => x.Id == productid).UnitPrice));
        }

        public void RemoveProduct(int productid, int quantity)
        {
            if (quantity <= 0)
            {
                throw new QuantityCannotBeNegativeException();
            }
            if (SC.Where(x => x.ProductID == productid).ToList().Count <= 0)
            {
                throw new CannotRemoveHigherQuantityThanAddedException();
            }
            else
            {
                if ((SC.First(x => x.ProductID == productid).Quantity - quantity) < 0)
                {
                    throw new CannotRemoveHigherQuantityThanAddedException();
                }
            }
        }

        public void PositiveQuantity(int quantity)
        {

        }

        [Test]
        public void CannotAddProductNotBelongingToRepository()
        {
            //To test if the product is not available in product list
            Assert.Throws<CannotAddProductNotBelongingToRepository>(() => AddProduct(6, 1));
        }

        [Test]
        public void QuantityCannotBeNegativeException()
        {
            //To test if the quantity when adding the shopping cart is more than 0 
            Assert.Throws<QuantityCannotBeNegativeException>(() => AddProduct(3, -1));

        }

         [Test]
        public void QuantityCannotBeNegativeExceptionDuringRemoveProduct()
        {            
            //To test if the quantity when removing the shopping cart is more than 0 
            Assert.Throws<QuantityCannotBeNegativeException>(() => RemoveProduct(3, -1));
        }

        [Test]
        public void HigherQuantityAddedThanAvailableException()
        {
            //To test if the quantity is more than 1 order 
            Assert.Throws<HigherQuantityAddedThanAvailableException>(() => AddProduct(1, 11));

        }

        [Test]
        public void CannotRemoveHigherQuantityThanAddedException()
        {
            AddProductTest(1, 3);

            //To test if the quantity is more than 1 order 
            Assert.Throws<CannotRemoveHigherQuantityThanAddedException>(() => RemoveProduct(1, 11));

            SC = new List<ShoppingCartItem>();

        }

        [Test]
        public void CannotRemoveOtherProductThatNotAdded()
        {
            AddProductTest(2, 3);

            //To test if the quantity is more than 1 order 
            Assert.Throws<CannotRemoveHigherQuantityThanAddedException>(() => RemoveProduct(1, 1));

            SC = new List<ShoppingCartItem>();
        }


    }

    internal class CannotRemoveHigherQuantityThanAddedException : Exception
    {

    }


    internal class HigherQuantityAddedThanAvailableException : Exception
    {

    }

    internal class QuantityCannotBeNegativeException : Exception
    {
    }

    internal class CannotAddProductNotBelongingToRepository : Exception
    {
    }
}