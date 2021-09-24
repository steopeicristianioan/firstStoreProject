using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emag.model
{
    class OrderDetails
    {
        private int id;
        private int orderId;
        private int productId;
        private int quantity;
        private double price;
        
        public OrderDetails(int id, int orderId, int productId, int quantity, double price)
        {
            this.id = id;
            this.orderId = orderId;
            this.productId = productId;
            this.quantity = quantity;
            this.price = price;
        }

        public int ID { get => this.id; set => this.id = value; }
        public int OrderId { get => this.orderId; set => this.orderId = value; }
        public int ProductId { get => this.productId; set => this.productId = value; }
        public int Quantity { get => this.quantity; set => this.quantity = value; }
        public double Price { get => this.price; set => this.price = value; }

        public override string ToString()
        {
            return this.id.ToString() + "|" + this.orderId.ToString() + "|" + this.productId.ToString() + "|" + this.quantity.ToString() + "|" + this.price.ToString();
        }
        public bool Equals(OrderDetails orderDetails) { return this.id.Equals(orderDetails.ID); }
    }
}
