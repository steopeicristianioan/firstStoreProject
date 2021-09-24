using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Emag.control;
using Emag.view;
using Emag.model;

namespace Emag.services
{
    class ServiceProfile
    {
        private ViewProfile view;
        private Customer customer;
        private ControlCustomers customers;
        public ServiceProfile(ViewProfile view) 
        {
            this.view = view;
            this.customers = new ControlCustomers();
            this.customer = customers.getById(this.view.UserID);
        }

        public void displayOrderDetails(Orders order)
        {
            this.view.Main.Controls.Clear();

            ControlOrderDetails details = new ControlOrderDetails();
            List<OrderDetails> cart = details.getCart(order.ID);
            double price = details.cartPrice(cart);

            Label start = new Label();
            start.Parent = this.view.Main;
            start.Location = new Point(10, 10);
            start.Width = 490;
            start.Height = 100;
            start.TextAlign = ContentAlignment.MiddleCenter;
            start.Font = new Font("Microsoft Sitka Small", 12, FontStyle.Regular);
            start.Text = "Comanda #" + order.ID + "\nSubtotal: " + price + " $";

            ControlOrders orders = new ControlOrders(this.view);
            int height = orders.loadDetails(cart);

            Button exit = new Button();
            exit.BackColor = Color.PaleGreen;
            exit.Parent = this.view.Main;
            exit.Location = new Point(400, height);
            exit.Width = 100;
            exit.Height = 50;
            exit.Text = "Iesi";
            exit.Click += new EventHandler(this.exitOrders_Click);
            for (int i = 1; i < this.view.Main.Controls.Count; i++)
                this.view.Main.Controls[i].Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
        }
        public void home_Click(Object sender, EventArgs e)
        {
            ViewProducts cart = new ViewProducts(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, this.view.UserID, this.view.Form, 1);
        }
        public void exitOrders_Click(Object sender, EventArgs e)
        {
            ControlOrders orders = new ControlOrders(this.view);
            orders.loadOrders(this.view.UserID);
        }
        public void orders_Click(Object sender, EventArgs e)
        {
            ControlOrders orders = new ControlOrders(this.view);
            orders.loadOrders(this.view.UserID);
            foreach (Control control in this.view.Main.Controls)
                control.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
        }
        public void details_Click(Object sender, EventArgs e, Orders orders)
        {
            displayOrderDetails(orders);
        }
        private void displayUserSettings()
        {
            this.view.Main.Controls.Clear();

            Label usernameInfo = new Label();
            usernameInfo.Parent = this.view.Main;
            usernameInfo.Location = new Point(10, 50);
            usernameInfo.Width = 220;
            usernameInfo.Height = 50;
            usernameInfo.Font = new Font("Microsoft Sitka Small", 12, FontStyle.Regular);
            usernameInfo.Text = "Acesta este username-ul tau: ";
            usernameInfo.TextAlign = ContentAlignment.TopCenter;

            TextBox username = new TextBox();
            username.Parent = this.view.Main;
            username.Location = new Point(240, 50);
            username.Width = 260;
            username.Font = usernameInfo.Font;
            username.Text = customer.Username;

            Label passwordInfo = new Label();
            passwordInfo.Parent = this.view.Main;
            passwordInfo.Location = new Point(10, 110);
            passwordInfo.Width = 180;
            passwordInfo.Height = 50;
            passwordInfo.Text = "Aceasta este parola ta: ";
            passwordInfo.TextAlign = ContentAlignment.TopCenter;
            passwordInfo.Font = usernameInfo.Font;

            TextBox password = new TextBox();
            password.Parent = this.view.Main;
            password.Location = new Point(200, 110);
            password.Width = 300;
            password.Text = customer.Password;
            password.Font = username.Font;

            Button save = new Button();
            save.BackColor = Color.PaleGreen;
            save.Parent = this.view.Main;
            save.Location = new Point(155, 200);
            save.Width = 200;
            save.Height = 100;
            save.Text = "Salveaza modificarile";
            save.Font = password.Font;
            save.Click += delegate (Object sender2, EventArgs e2) { this.saveSettings_Click(sender2, e2, username.Text, password.Text, username, password); };

            Button exit = new Button();
            exit.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
            exit.BackColor = Color.PaleGreen;
            exit.Parent = this.view.Main;
            exit.Location = new Point(400, 320);
            exit.Width = 100;
            exit.Height = 55;
            exit.Text = "Iesi";
            exit.Click += delegate (Object sender2, EventArgs e2) { this.exitSetting_Click(sender2, e2, username.Text, password.Text); };
        }
        public void saveSettings_Click(Object sender, EventArgs e, string newName, string newPassword, TextBox username, TextBox password)
        {
            this.customer.Password = newPassword;
            this.customer.Username = newName;
            this.customers.write();
            username.Text = newName;
            password.Text = newPassword;
        }
        public void exitSetting_Click(Object sender, EventArgs e, string newName, string newPassword)
        {
            if (!newName.Equals(this.customer.Username) || !newPassword.Equals(this.customer.Password))
            {
                DialogResult result = MessageBox.Show("Esti sigur ca nu vrei sa salvezi noile date?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result.Equals(DialogResult.Yes))
                {
                    this.view.loadMain();
                }
            }
            else this.view.loadMain();
        }
        public void settings_Click(Object sender, EventArgs e)
        {
            displayUserSettings();
        }
        public void cart_Click(Object sende, EventArgs e)
        {
            ViewCart cart = new ViewCart(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, this.view.UserID, this.view.Form);
        }
    }
}
