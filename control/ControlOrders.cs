using Emag.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Emag.view;

namespace Emag.control
{
    class ControlOrders
    {
        private List<Orders> allOrders;
        private string url = Application.StartupPath + "/orders.txt";
        private ViewProfile view;
        public ControlOrders()
        {
            this.allOrders = new List<Orders>();
            read();
        }
        public ControlOrders(ViewProfile view) : this()
        {
            this.view = view;
        }

        public int LastId { get => allOrders[allOrders.Count - 1].ID; }

        public void read()
        {
            this.allOrders.Clear();
            StreamReader reader = new StreamReader(url);
            string line = string.Empty;
            while((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split('|');
                Orders orders = new Orders(int.Parse(values[0]), int.Parse(values[1]), values[2], int.Parse(values[3]), int.Parse(values[4]));
                this.allOrders.Add(orders);
            }
            reader.Close();
        }
        public void write()
        {
            StreamWriter writer = new StreamWriter(url);
            string result = string.Empty;
            foreach(Orders orders in allOrders)
            {
                result += orders.ToString();
                result += "\n";
            }
            writer.Write(result);
            writer.Close();
        }
        public void logOrders()
        {
            foreach(Orders orders in allOrders)
                Console.WriteLine(orders.ToString());
        }

        public Orders getById(int id)
        {
            Orders notFound = new Orders(-1, -1, "", 0, 2);
            foreach (Orders orders in allOrders)
                if (orders.ID.Equals(id))
                    return orders;
            return notFound;
        }
        public void add(Orders orders)
        {
            this.allOrders.Add(orders);
            write();
        }
        public void remove(Orders orders)
        {
            this.allOrders.Remove(orders);
            write();
        }

        public int getLastOrderId(int customerId)
        {
            foreach(Orders order in allOrders)
            {
                if (order.CustomerId.Equals(customerId) && order.OrderSent.Equals(0))
                    return order.ID;
            }
            return -1;
        }

        public int loadDetails(List<OrderDetails> cart)
        {
            int x = 10, y = 120;
            foreach(OrderDetails detail in cart)
            {
                Panel card = new Panel();
                CartProductCard detailCard = new CartProductCard(detail, card, this.view);
                detailCard.ForCart = 0;
                detailCard.setLocation(x, y);
                detailCard.loadCard();
                y += 90;
            }
            return y;
        }
        public List<Orders> customerOrders(int customerId)
        {
            List<Orders> result = new List<Orders>();
            foreach (Orders order in allOrders)
                if (order.CustomerId.Equals(customerId) && order.OrderSent.Equals(1))
                    result.Add(order);
            return result;
        }
        public void loadOrders(int customerId)
        {
            this.view.Main.Controls.Clear();
            List<Orders> orders = customerOrders(customerId);
            int x = 20, y = 20;
            int count = 0;
            foreach(Orders order in orders)
            {
                count++;
                Panel card = new Panel();
                OrderCard orderCard = new OrderCard(card, this.view, order);
                orderCard.setLocation(x, y);
                orderCard.loadCard();
                if (count == 3)
                {
                    x = 20;
                    y += 160;
                    count = 0;
                }
                else x += 160;
            }
        }
    }
}
