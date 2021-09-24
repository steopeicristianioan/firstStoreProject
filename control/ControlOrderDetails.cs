using Emag.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Emag.view;
using System.Drawing;

namespace Emag.control
{
    class ControlOrderDetails
    {
        private List<OrderDetails> allOrderDetails;
        private string url = Application.StartupPath + "/orderDetails.txt";
        private ViewCart view;
        public ControlOrderDetails(ViewCart view)
        {
            this.view = view;
            this.allOrderDetails = new List<OrderDetails>();
            read();
        }
        public ControlOrderDetails()
        {
            this.allOrderDetails = new List<OrderDetails>();
            read();
        }

        public int LastId { get => this.allOrderDetails[allOrderDetails.Count - 1].ID; }

        public void read()
        {
            this.allOrderDetails.Clear();
            StreamReader reader = new StreamReader(url);
            string line = string.Empty;
            while((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split('|');
                OrderDetails details = new OrderDetails(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), double.Parse(values[4]));
                this.allOrderDetails.Add(details);
            }
            reader.Close();
        }
        public void write()
        {
            StreamWriter writer = new StreamWriter(url);
            string result = string.Empty;
            foreach(OrderDetails details in allOrderDetails)
            {
                result += details.ToString();
                result += "\n";
            }
            writer.Write(result);
            writer.Close();
        }
        public void logOrderDetails()
        {
            foreach(OrderDetails details in allOrderDetails)
                Console.WriteLine(details.ToString());
        }

        public OrderDetails getById(int id)
        {
            OrderDetails notFound = new OrderDetails(-1, -1, -1, -1, 0.0);
            foreach (OrderDetails details in allOrderDetails)
                if (details.ID.Equals(id))
                    return details;
            return notFound;
        }
        public void add(OrderDetails details)
        {
            this.allOrderDetails.Add(details);
            write();
        }
        public void remove(OrderDetails details)
        {
            this.allOrderDetails.Remove(details);
            write();
        }
        
        public List<OrderDetails> getCart(int orderId)
        {
            List<OrderDetails> cart = new List<OrderDetails>();
            foreach(OrderDetails details in allOrderDetails)
            {
                if (details.OrderId.Equals(orderId))
                    cart.Add(details);
            }
            return cart;
        }
        public int idIfIsInCart(List<OrderDetails> cart, int productId)
        {
            foreach(OrderDetails details in cart)
            {
                if (details.ProductId.Equals(productId))
                    return details.ID;
            }
            return -1;
        }

        public double cartPrice(List<OrderDetails> cart)
        {
            double result = 0.0;
            foreach (OrderDetails details in cart)
            {
                result += details.Price;
            }
            return result;
        }
        public int loadCart(List<OrderDetails> cart, bool withCards)
        {
            int y = 10;

            if (cart.Count.Equals(0))
            {
                Label message = new Label();
                message.BackColor = Color.PaleTurquoise;
                message.Parent = this.view.Main;
                message.Location = new Point(10, y);
                message.Width = this.view.Main.Width - 23;
                message.Height = this.view.Main.Height - 80;
                message.Text = "Cosul tau este gol, adauga niste produse in el!";
                message.TextAlign = ContentAlignment.MiddleCenter;
                message.Font = new Font("Microsoft San Serif", 20, FontStyle.Regular);
                message.BorderStyle = BorderStyle.FixedSingle;
                return 285;
            }
            else
            {
                int x = 65;
                double cost = 0.0;
                foreach (OrderDetails details in cart)
                {
                    if (withCards)
                    {
                        cost += details.Price;
                        Panel card = new Panel();
                        CartProductCard cartCard = new CartProductCard(details, card, this.view);
                        cartCard.setLocation(x, y);
                        cartCard.loadCard();
                    }
                    y += 90;
                }
                Label total = new Label();
                total.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
                total.Parent = this.view.Main;
                total.Location = new Point(465, y);
                total.Width = 185;
                total.Height = 30;
                total.BorderStyle = BorderStyle.FixedSingle;
                total.Text = "Subtotal: " + this.cartPrice(cart).ToString() + " $";
                total.TextAlign = ContentAlignment.MiddleCenter;
                return y;
            }
        }
    }
}
