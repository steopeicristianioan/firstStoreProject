using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Emag.control;
using Emag.model;
using Emag.services;

namespace Emag.view
{
    class ViewCart : View
    {
        private ServiceCart service;

        public ViewCart(Panel header, Panel main, Panel aside, Panel footer, int id, Form1 form) : base(header, main, aside, footer, id, form) 
        {
            service = new ServiceCart(this);

            header.Location = new Point(0, 0);
            header.Height = 100;
            header.Width = 715;
            header.BorderStyle = BorderStyle.FixedSingle;

            main.Controls.Clear();
            main.Location = new Point(0, 105);
            main.Height = 385;
            main.Width = 715;
            main.BorderStyle = BorderStyle.FixedSingle;
            main.AutoScroll = true;

            aside.Width = 0;
            aside.Height = 0;

            footer.Location = new Point(0, 495);
            footer.Width = 715;
            footer.Height = 100;
            footer.BorderStyle = BorderStyle.FixedSingle;

            loadHeader();
            loadMain();
        }

        public override void loadMain()
        {
            this.main.Controls.Clear();

            ControlOrderDetails details = new ControlOrderDetails(this);
            ControlOrders orders = new ControlOrders();

            int lastOrderId = orders.getLastOrderId(this.currentUserId);
            List<OrderDetails> cart = details.getCart(lastOrderId);
            int height = details.loadCart(cart, true);

            Button back = new Button();
            back.Parent = this.main;
            back.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
            back.BackColor = Color.PaleGreen;
            if (!cart.Count.Equals(0))
            {
                back.Location = new Point(515, height + 40);
                back.Width = 135;
            }
            else
            {
                back.Location = new Point(605, height + 40);
                back.Width = 100;
            }
            back.Height = 50;
            back.Text = "Acasa";
            back.Click += new EventHandler(service.backButton_Click);

            if (!cart.Count.Equals(0))
            {
                Button send = new Button();
                send.BackColor = Color.PaleGreen;
                send.Parent = this.Main;
                send.Location = new Point(65, height + 40);
                send.Width = 150;
                send.Height = 50;
                send.Text = "Trimite comanda";
                send.Click += delegate (Object sender2, EventArgs e2) { service.sendOrder_Click(sender2, e2, lastOrderId); };
            }

            if(!cart.Count.Equals(0))
                foreach (Control control in this.main.Controls)
                    control.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
        }
        protected override void loadHeader()
        {
            this.header.Controls.Clear();

            ControlCustomers customers = new ControlCustomers();

            Label welcome = new Label();
            welcome.Parent = header;
            welcome.Location = new Point(10, 10);
            welcome.Width = 605;
            welcome.Height = 80;
            welcome.AutoSize = false;
            welcome.Text = "Bun venit in cosul tau, " + customers.getById(currentUserId).Username;
            welcome.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Regular);
            welcome.TextAlign = ContentAlignment.MiddleCenter;
            welcome.BorderStyle = BorderStyle.FixedSingle;

            Button profile = new Button();
            profile.Parent = header;
            profile.Location = new Point(624, 9);
            profile.Width = 80;
            profile.Height = 81;
            profile.Image = Image.FromFile(Application.StartupPath + @"\userLogo.png");
            profile.TabStop = false;
            profile.Click += new EventHandler(service.profile_Click);
            ToolTip tip = new ToolTip();
            tip.AutoPopDelay = 2000;
            tip.SetToolTip(profile, "Vezi profilul tau");
        }
    }
}
