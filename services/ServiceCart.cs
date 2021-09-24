using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emag.control;
using Emag.model;
using Emag.view;

namespace Emag.services
{
    class ServiceCart
    {
        private ViewCart view;
        public ServiceCart(ViewCart view)
        {
            this.view = view;
        }

        public void backButton_Click(Object sender, EventArgs e)
        {
            ViewProducts products = new ViewProducts(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, this.view.UserID, this.view.Form, 1);
        }
        public void minusButton_Click(Object sender, EventArgs e, int detailId, Label quantity)
        {
            ControlOrderDetails details = new ControlOrderDetails(this.view);
            ControlOrders orders = new ControlOrders();
            ControlProducts products = new ControlProducts();
            Orders order = orders.getById(orders.getLastOrderId(this.view.UserID));
            OrderDetails detail = details.getById(detailId);
            Product product = products.getById(detail.ProductId);

            detail.Quantity--;
            product.Stock++;
            detail.Price -= product.Price;
            if (detail.Quantity.Equals(0) && order.Ammount.Equals(1))
            {
                details.remove(detail);
                orders.remove(order);
                this.view.loadMain();
            }
            else if(detail.Quantity.Equals(0) && !order.Ammount.Equals(1))
            {
                details.remove(detail);
                order.Ammount--;
                orders.write();
                this.view.loadMain();
            }
            else if((!detail.Quantity.Equals(0) && !order.Ammount.Equals(1)) || (!detail.Quantity.Equals(0) && order.Ammount.Equals(1)))
            {
                quantity.Text = detail.Quantity.ToString();
                orders.write();
                details.write();

                orders.read();
                details.read();

                List<OrderDetails> cart = details.getCart(order.ID);
                this.view.Main.Controls[cart.Count].Text = "Subtotal: " + details.cartPrice(cart).ToString() + " $";
            }
            products.write();
        }
        public void plusButton_Click(Object sender, EventArgs e, int detailId, Label quantity)
        {
            ControlOrderDetails details = new ControlOrderDetails(this.view);
            ControlOrders orders = new ControlOrders();
            ControlProducts products = new ControlProducts();
            Orders order = orders.getById(orders.getLastOrderId(this.view.UserID));
            OrderDetails detail = details.getById(detailId);
            Product product = products.getById(detail.ProductId);

            if(product.Stock >= 1)
            {
                detail.Quantity++;
                product.Stock--;
                detail.Price += product.Price;
                quantity.Text = detail.Quantity.ToString();
                details.write();
                List<OrderDetails> cart = details.getCart(order.ID);
                this.view.Main.Controls[cart.Count].Text = "Subtotal: " + details.cartPrice(cart).ToString() + " $";
                products.write();
            }
            else
            {
                MessageBox.Show("The product is out of stock");
            }
        }
        public void remove_Click(Object sender, EventArgs e, int detailId)
        {
            ControlOrderDetails details = new ControlOrderDetails(this.view);
            OrderDetails detail = details.getById(detailId);

            ControlOrders orders = new ControlOrders();
            Orders order = orders.getById(detail.OrderId);

            ControlProducts products = new ControlProducts();
            Product product = products.getById(detail.ProductId);

            product.Stock += detail.Quantity;
            details.remove(detail);
            order.Ammount--;
            if(order.Ammount.Equals(0))
                orders.remove(order);
            orders.write();
            details.write();
            products.write();
            this.view.loadMain();
            if (!order.Ammount.Equals(0))
            {
                List<OrderDetails> cart = details.getCart(order.ID);
                this.view.Main.Controls[cart.Count].Text = "Subtotal: " + details.cartPrice(cart).ToString() + " $";
            }
        }
        public void sendOrder_Click(Object sender, EventArgs e, int orderId)
        {
            ControlOrders orders = new ControlOrders();
            Orders order = orders.getById(orderId);

            order.OrderSent = 1;
            orders.write();
            MessageBox.Show("Comanda #" + orderId + " a fost inregistrata cu succes");
            this.view.loadMain();
        }
        public void profile_Click(Object sender, EventArgs e)
        {
            ViewProfile view = new ViewProfile(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, this.view.UserID, this.view.Form);
        }
    }
}
