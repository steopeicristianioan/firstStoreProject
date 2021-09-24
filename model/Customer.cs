using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emag.model
{
    class Customer
    {
        private int id;
        private string username;
        private string password;
        private string phone;
        private string shippingAdress;

        public Customer(int id, string username, string password, string phone, string shippingAdress)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.phone = phone;
            this.shippingAdress = shippingAdress;
        }

        public int ID { get => this.id; set => this.id = value; }
        public string Username { get => this.username; set => this.username = value; }
        public string Password { get => this.password; set => this.password = value; }
        public string Phone { get => this.phone; set => this.phone = value; }
        public string ShippingAdress { get => this.shippingAdress; set => this.shippingAdress = value; }

        public override string ToString()
        {
            return this.id.ToString() + "|" + this.username + "|" + this.password + "|" + this.phone + "|" + this.shippingAdress;
        }
        public bool Equals(Customer customer) { return this.id.Equals(customer.id); }
    }
}
