using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emag.model;
using System.Drawing;
using Emag.view;
using Emag.control;

namespace Emag.services
{
    class ServiceHome
    {
        private ViewProducts view;
        public ServiceHome(ViewProducts view)
        {
            this.view = view;
        }
        public ServiceHome() { }

        public void loadProductDetails(Product product)
        {
            this.view.Main.Controls.Clear();

            PictureBox box = new PictureBox();
            box.Parent = this.view.Main;
            box.Location = new Point(145, 10);
            box.Width = 200;
            box.Height = 165;
            box.Image = product.Image;
            box.SizeMode = PictureBoxSizeMode.StretchImage;

            Label name = new Label();
            name.Parent = this.view.Main;
            name.Location = new Point(155, 185);
            name.Width = 180;
            name.Height = 30;
            name.Text = product.Name;
            name.TextAlign = ContentAlignment.MiddleCenter;

            Label price = new Label();
            price.Parent = this.view.Main;
            price.Location = new Point(165, 225);
            price.Width = 160;
            price.Height = 30;
            price.BorderStyle = BorderStyle.FixedSingle;
            price.Text = product.Price.ToString() + "$";
            price.TextAlign = ContentAlignment.MiddleCenter;

            Label description = new Label();
            description.Parent = this.view.Main;
            description.Location = new Point(10, 265);
            description.Width = 490;
            description.Height = 50;
            description.Text = product.Description +"\n" + (product.Stock > 0 ? "In stoc" : "Indisponibil");
            description.TextAlign = ContentAlignment.MiddleCenter;

            Button add = new Button();
            add.Parent = this.view.Main;
            if (product.Stock == 0 || this.view.HasAccount != 1)
            {
                add.Enabled = false;
                add.BackColor = Color.PaleGoldenrod;
            }
            if(add.Enabled == true)
                add.BackColor = Color.PaleGreen;
            add.Location = new Point(205, 325);
            add.Width = 100;
            add.Height = 50;
            add.Text = "Adauga in cos";
            add.Click += delegate (Object sender2, EventArgs e2) { this.addToCart_Click(sender2, e2, product); };

            Button exit = new Button();
            exit.BackColor = Color.PaleGreen;
            exit.Parent = this.view.Main;
            exit.Location = new Point(350, 325);
            exit.Width = 150;
            exit.Height = 50;
            exit.Text = "Inapoi la pagina principala";
            exit.Click += new EventHandler(this.exit_Click);

            foreach (Control control in this.view.Main.Controls)
                control.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
        }
        private void exit_Click(Object sender, EventArgs e)
        {
            this.view.loadMain();
        }
        private void addToCart_Click(Object sender, EventArgs e, Product product)
        {
            ControlCustomers customers = new ControlCustomers();
            Customer currentCustomer = customers.getById(this.view.UserID);

            ControlOrders orders = new ControlOrders();
            int lastUnfinishedOrderID = orders.getLastOrderId(currentCustomer.ID);

            ControlOrderDetails details = new ControlOrderDetails();
            Orders order;

            if (lastUnfinishedOrderID.Equals(-1))
            {
                order = new Orders(orders.LastId + 1, currentCustomer.ID, currentCustomer.ShippingAdress, 1, 0);
                orders.add(order);
            }
            else
            {
                order = orders.getById(lastUnfinishedOrderID);
            }

            OrderDetails detail;
            int idIfExistsInCart = details.idIfIsInCart(details.getCart(order.ID), product.ID);
            ControlProducts products = new ControlProducts();
            Product product1 = products.getById(product.ID);
            product1.Stock--;
            products.write();
            if (idIfExistsInCart.Equals(-1))
            {
                detail = new OrderDetails(details.LastId + 1, order.ID, product.ID, 1, product.Price);
                details.add(detail);
            }
            else
            {
                detail = details.getById(idIfExistsInCart);
                detail.Quantity++;
                detail.Price += product.Price;
                details.write();
            }

            details.read();
            order.Ammount = details.getCart(order.ID).Count;
            orders.write();
            MessageBox.Show("Produsul a fost adaugat cu succes in cosul dvs.");
        }

        public void addEventsOnTextBox(TextBox box, string textIfEmpty, bool isNumeric, int priceIndex)
        {
            box.Click += new EventHandler(this.textBox_Click);
            box.MouseLeave += delegate (Object sender2, EventArgs e2) { this.textBox_Leave(sender2, e2, textIfEmpty); };
            if (isNumeric)
                box.TextChanged += delegate (Object sender2, EventArgs e2) { this.textBox_CheckIfNumber(sender2, e2, priceIndex); };
        }
        private void textBox_Click(Object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if(box.Text.Equals("Cauta dupa nume") || box.Text.Equals("De la:") || box.Text.Equals("Pana la:"))
                box.Text = string.Empty;
        }
        private void textBox_Leave(Object sender, EventArgs e, string text)
        {
            TextBox box = (TextBox)sender;
            if (box.Text.Equals(string.Empty))
            {
                ControlFilters.Name = string.Empty;
                box.Text = text;
            }
        }
        private void textBox_CheckIfNumber(Object sender, EventArgs e, int index)
        {
            TextBox box = (TextBox)sender;
            if (box.Text.Equals("De la:") || box.Text.Equals("Pana la:"))
                box.ForeColor = Color.Black;
            else if ((!double.TryParse(box.Text, out double number) || number < 0))
                box.ForeColor = Color.Red;
            else
            {
                box.ForeColor = Color.Lime;
                if (index == 1)
                    ControlFilters.Price1 = number;
                else if(index == 2) ControlFilters.Price2 = number;
            }
        }
        public void nameTextBox_TextChanged(Object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(!textBox.Text.Equals("Cauta dupa nume"))
                ControlFilters.Name = textBox.Text;
        }

        public void performSearch(TextBox name, TextBox price1, TextBox price2)
        {
            ControlProducts products = new ControlProducts(this.view);
            if (name.Text.Equals("Cauta dupa nume") && price1.Text.Equals("De la:") && price2.Text.Equals("Pana la:") && !price1.ForeColor.Equals(Color.Red) && !price2.ForeColor.Equals(Color.Red))
            {
                products.loadAllProducts();
                return;
            }
            bool ok = true;
            if (price1.ForeColor.Equals(Color.Red))
            {
                price1.Text = "Valoare invalida!";
                ok = false;
            }
            if (price2.ForeColor.Equals(Color.Red))
            {
                price2.Text = "Valoare invalida!";
                ok = false;
            }
            if (ok)
                products.loadProducts(ControlFilters.Name, ControlFilters.Price1, ControlFilters.Price2);
            else products.loadProducts("", -1, -2);
        }
        public void searchButton_Click(Object sender, EventArgs e, TextBox name, TextBox price1, TextBox price2)
        {
            this.performSearch(name, price1, price2);
        }

        public void removeFilters_Click(Object sender, EventArgs e, TextBox name, TextBox price1, TextBox price2)
        {
            name.Text = "Cauta dupa nume";
            price1.Text = "De la:";
            price2.Text = "Pana la:";
            ControlFilters.Name = string.Empty;
            ControlFilters.Price1 = 0;
            ControlFilters.Price2 = 10000000000;
            this.view.loadMain();
        }

        public void viewCart_Click(Object sender, EventArgs e)
        {
            ViewCart cart = new ViewCart(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, this.view.UserID, this.view.Form);
        }
        public void profile_Click(Object sender, EventArgs e)
        {
            if(this.view.HasAccount == 1)
            {
                ViewProfile view = new ViewProfile(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, this.view.UserID, this.view.Form);
            }
            else
            {
                ViewLogIn logIn = new ViewLogIn(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, -1, this.view.Form);
            }
        }

        public void logOut_Click(Object sender, EventArgs e)
        {
            ViewProducts products = new ViewProducts(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, -1, this.view.Form, 0);
        }

         
    }
}
