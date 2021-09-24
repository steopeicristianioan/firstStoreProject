using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emag.model
{
    class Orders
    {
        private int id;
        private int customerId;
        private string shippingAdress;
        private int ammount;
        private int orderSent;

        public Orders(int id, int customerId, string shippingAdress, int ammount, int sent)
        {
            this.id = id;
            this.customerId = customerId;
            this.shippingAdress = shippingAdress;
            this.ammount = ammount;
            this.orderSent = sent;
        }

        public int ID { get => this.id; set => this.id = value; }
        public int CustomerId { get => this.customerId; set => this.customerId = value; }
        public string ShippingAdress { get => this.shippingAdress; set => this.shippingAdress = value; }
        public int Ammount { get => this.ammount; set => this.ammount = value; }
        public int OrderSent { get => this.orderSent; set => this.orderSent = value; }

        public override string ToString()
        {
            return this.id.ToString() + "|" + this.customerId.ToString() + "|" + this.shippingAdress + "|" + this.ammount.ToString() + "|" + this.orderSent.ToString();
        }
        public bool Equals(Orders orders) { return this.id.Equals(orders.ID); }
    }
}
