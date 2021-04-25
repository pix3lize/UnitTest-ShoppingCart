using System.Collections.Generic;

namespace ShoppingCart
{
    public class ProductRepository
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AvailibilityQuantity { get; set; }

        public int UnitPrice { get; set; }

        public ProductRepository(int id, string name, int availbility, int unitprice)
        {
            this.Id = id;
            this.Name = name;
            this.AvailibilityQuantity = availbility;
            this.UnitPrice = unitprice;

        }
    }

    public class ShoppingCartItem
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }

        public ShoppingCartItem(int productid, int quantity, int unitprice)
        {
            this.ProductID = productid;
            this.Quantity = quantity;
            this.UnitPrice = unitprice;
        }
    }
}