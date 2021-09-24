using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Emag.services;

namespace Emag.view
{
    class ViewProfile : View
    {
        ServiceProfile service;
        public ViewProfile(Panel header, Panel main, Panel aside, Panel footer, int userId, Form1 form) : base(header, main, aside, footer, userId, form) 
        {
            this.service = new ServiceProfile(this);

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
            loadAside();
            loadMain();
        }

        protected override void loadAside()
        {
            this.aside.Controls.Clear();

            Button orders = new Button();
            orders.Parent = this.aside;
            orders.Location = new Point(10, 10);
            orders.Width = 180;
            orders.Height = 83;
            orders.Text = "Comenzile mele";
            orders.Click += new EventHandler(service.orders_Click);

            Button settings = new Button();
            settings.Parent = this.aside;
            settings.Location = new Point(10, 103);
            settings.Width = 180;
            settings.Height = 83;
            settings.Text = "Setari";
            settings.Click += new EventHandler(service.settings_Click);

            Button home = new Button();
            home.Parent = this.aside;
            home.Location = new Point(10, 193);
            home.Width = 180;
            home.Height = 83;
            home.Text = "Acasa";
            home.Click += new EventHandler(service.home_Click);

            Button cart = new Button();
            cart.Parent = this.aside;
            cart.Location = new Point(10, 283);
            cart.Width = 180;
            cart.Height = 88;
            cart.Text = "Cosul meu";
            cart.Click += new EventHandler(service.cart_Click);

            foreach (Control control in this.aside.Controls)
                control.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
            foreach (Button button in this.aside.Controls)
                button.BackColor = Color.PaleGreen;
        }
        public override void loadMain()
        {
            this.main.Controls.Clear();

            PictureBox picture = new PictureBox();
            picture.Parent = this.main;
            picture.Location = new Point(10, 10);
            picture.Width = 490;
            picture.Height = 365;
            picture.Image = Image.FromFile(Application.StartupPath + @"\pictures\userBackground.png");
            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            Label message = new Label();
            message.Parent = picture;
            message.Location = new Point(135, 250);
            message.Width = 220;
            message.Height = 80;
            message.ForeColor = Color.White;
            message.Font = new Font("Microsoft San Serif", 10, FontStyle.Regular);
            message.Text = "Vizualizeaza si modifica datele profilului tau";
            message.TextAlign = ContentAlignment.BottomCenter;
            message.BackColor = Color.Transparent;
            
        }
        protected override void loadHeader()
        {
            this.header.Controls.Clear();

            Label welcome = new Label();
            welcome.Parent = this.header;
            welcome.Location = new Point(10, 10);
            welcome.Width = 695;
            welcome.Height = 80;
            welcome.Text = "Aici ai toate informatiile necesare despre profilul tau!";
            welcome.TextAlign = ContentAlignment.MiddleCenter;
            welcome.Font = new Font("Microsoft Sitka Small", 15, FontStyle.Regular);
        }
    }
}
