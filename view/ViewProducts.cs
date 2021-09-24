using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Emag.control;
using Emag.services;

namespace Emag.view
{
    class ViewProducts : View
    {
        private ServiceHome service;
        private int hasAccount = 1;
        public int HasAccount { get => this.hasAccount; set => this.hasAccount = value; }
        public ViewProducts(Panel header, Panel main, Panel aside, Panel footer, int id, Form1 form, int status) : base(header, main, aside, footer, id, form) 
        {
            this.hasAccount = status;
            this.service = new ServiceHome(this);

            header.Location = new Point(0, 0);
            header.Height = 100;
            header.Width = 715;
            header.BorderStyle = BorderStyle.FixedSingle;

            aside.Location = new Point(0, 105);
            aside.Width = 200;
            aside.Height = 385;
            aside.BorderStyle = BorderStyle.FixedSingle;

            main.Location = new Point(205, 105);
            main.Width = 510;
            main.Height = 385;
            main.BorderStyle = BorderStyle.FixedSingle;
            main.AutoScroll = true;

            footer.Location = new Point(0, 495);
            footer.Width = 715;
            footer.Height = 100;
            footer.BorderStyle = BorderStyle.FixedSingle;

            loadHeader();
            loadMain();
            loadAside();
            loadFooter();
        }

        protected override void loadAside()
        {
            this.aside.Controls.Clear();
            this.aside.BackColor = Color.BlanchedAlmond;

            ControlFilters.Name = string.Empty;
            ControlFilters.Price1 = 0;
            ControlFilters.Price2 = 10000000000;

            Label nameFilter = new Label();
            nameFilter.Parent = aside;
            nameFilter.Location = new Point(50, 10);
            nameFilter.Width = 100;
            nameFilter.Height = 55;
            nameFilter.Text = "Configureaza filtrul de nume";
            nameFilter.TextAlign = ContentAlignment.MiddleCenter;
            nameFilter.AutoSize = false;

            TextBox nameInput = new TextBox();
            nameInput.Parent = aside;
            nameInput.Location = new Point(10, 75);
            nameInput.Width = 180;
            nameInput.Height = 20;
            nameInput.Text = "Cauta dupa nume";
            nameInput.TextChanged += new EventHandler(service.nameTextBox_TextChanged);
            service.addEventsOnTextBox(nameInput, "Cauta dupa nume", false, 0);

            Label priceFilter = new Label();
            priceFilter.Parent = aside;
            priceFilter.Location = new Point(50, 105);
            priceFilter.Width = 100;
            priceFilter.Height = 55;
            priceFilter.Text = "Configureaza intervalul de pret ($)";
            priceFilter.TextAlign = ContentAlignment.MiddleCenter;

            TextBox leftPrice = new TextBox();
            leftPrice.Parent = aside;
            leftPrice.Location = new Point(10, 170);
            leftPrice.Width = 85;
            leftPrice.Height = 20;
            leftPrice.Text = "De la:";
            leftPrice.TextAlign = HorizontalAlignment.Center;
            service.addEventsOnTextBox(leftPrice, "De la:", true, 1);

            TextBox rightPrice = new TextBox();
            rightPrice.Parent = aside;
            rightPrice.Location = new Point(105, 170);
            rightPrice.Width = 85;
            rightPrice.Height = 20;
            rightPrice.Text = "Pana la:";
            rightPrice.TextAlign = leftPrice.TextAlign;
            service.addEventsOnTextBox(rightPrice, "Pana la:", true, 2);

            Button search = new Button();
            search.BackColor = Color.PaleGreen;
            search.Parent = aside;
            search.Location = new Point(9, 200);
            search.Width = 181;
            search.Height = 50;
            search.Text = "Cauta folosind filtrele curente";
            search.Click += delegate (Object sender2, EventArgs e2) { service.searchButton_Click(sender2, e2, nameInput, leftPrice, rightPrice); };

            Button erase = new Button();
            erase.BackColor = Color.PaleGreen;
            erase.Parent = aside;
            erase.Location = new Point(9, 260);
            erase.Width = 181;
            erase.Height = 50;
            erase.Text = "Sterge toate filtrele";
            erase.Click += delegate (Object sender2, EventArgs e2) { service.removeFilters_Click(sender2, e2, nameInput, leftPrice, rightPrice); };

            if (this.hasAccount == 1)
            {
                Button cart = new Button();
                cart.BackColor = Color.PaleGreen;
                cart.Parent = aside;
                cart.Location = new Point(9, 320);
                cart.Width = 181;
                cart.Height = 55;
                cart.Text = "Vezi cosul tau";
                cart.Click += new EventHandler(service.viewCart_Click);
            }
            foreach (Control control in this.aside.Controls)
                control.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
        }
        protected override void loadHeader()
        {
            this.header.Controls.Clear();
            this.header.BackColor = Color.BlanchedAlmond;

            Label message = new Label();
            message.Parent = this.header;
            message.Location = new Point(10, 10);
            message.Width = 605;
            message.Height = 80;
            message.Font = new Font("Microsoft Sitka Small", 15, FontStyle.Regular);
            message.TextAlign = ContentAlignment.MiddleCenter;

            Button profile = new Button();
            profile.Parent = header;
            profile.Location = new Point(624, 9);
            profile.Width = 80;
            profile.Height = 81;
            profile.Click += new EventHandler(service.profile_Click);
            profile.TabStop = false;
            profile.BackColor = Color.PaleGreen;

            if (this.hasAccount == 1)
            {
                ControlCustomers customers = new ControlCustomers();

                message.Text = "Bun venit in contul tau, " + customers.getById(currentUserId).Username;
                profile.Image = Image.FromFile(Application.StartupPath + @"\userLogo.png");
                ToolTip tip = new ToolTip();
                tip.AutoPopDelay = 2000;
                tip.SetToolTip(profile, "Vezi profilul tau");
            }
            else
            {
                message.Text = "Simte-te liber sa cauti produse!";
                profile.Text = "Log in";
                profile.Font = new Font("Microsoft Sitka Small", 12, FontStyle.Regular);
            }
        }
        public override void loadMain()
        {
            this.main.Controls.Clear();
            main.BackColor = Color.BlanchedAlmond;

            ControlProducts products = new ControlProducts(this);
            products.loadAllProducts();
        }
        protected override void loadFooter()
        {
            this.footer.Controls.Clear();
            this.footer.BackColor = Color.BlanchedAlmond;

            Label trademark = new Label();
            trademark.Parent = footer;
            trademark.Location = new Point(355, 10);
            trademark.Width = 350;
            trademark.Height = 80;
            trademark.AutoSize = false;
            trademark.Text = "Cristi Sports Store, toate drepturile rezervate";
            trademark.TextAlign = ContentAlignment.BottomRight;
            trademark.Font = new Font("Microsoft Sant Serif", 12, FontStyle.Regular);

            if(hasAccount == 1)
            {
                Button logout = new Button();
                logout.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
                logout.BackColor = Color.PaleGreen;
                logout.Parent = footer;
                logout.Location = new Point(10, 10);
                logout.Width = 181;
                logout.Height = 80;
                logout.Text = "Log out";
                logout.Click += new EventHandler(service.logOut_Click);
            }
        }

        
    }
}
