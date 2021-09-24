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
    class ViewLogIn : View
    {
        private ServiceLogIn service;
        public ViewLogIn(Panel header, Panel main, Panel aside, Panel footer, int userId, Form1 form) : base(header, main, aside, footer, userId, form)
        {
            this.service = new ServiceLogIn(this);

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

            this.Main.AutoScroll = false;

            loadHeader();
            loadMain();
            loadFooter();
        }

        protected override void loadHeader()
        {
            this.header.Controls.Clear();

            Label message = new Label();
            message.Parent = this.header;
            message.Location = new Point(10, 10);
            message.Width = 695;
            message.Height = 80;
            message.Font = new Font("Microsoft San Serif", 15, FontStyle.Regular);
            message.Text = "Da-ti login ca sa intri in contul tau!";
            message.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void loadUsernameLabel()
        {
            Label username = new Label();
            username.Parent = this.main;
            username.Location = new Point(50, 100);
            username.Width = 200;
            username.Height = 50;
            username.Font = new Font("Microsoft San Serif", 12, FontStyle.Regular);
            username.Text = "Introdu username-ul tau:";
        }
        private void loadUsernameTextBox()
        {
            TextBox nameInput = new TextBox();
            nameInput.Parent = this.main;
            nameInput.Font = new Font("Microsoft Sitka Small", 12, FontStyle.Regular);
            nameInput.Location = new Point(260, 100);
            nameInput.Width = 340;
        }
        private void loadPasswordLabel()
        {
            Label password = new Label();
            password.Parent = this.main;
            password.Location = new Point(50, 150);
            password.Width = 200;
            password.Height = 50;
            password.Font = new Font("Microsoft San Serif", 12, FontStyle.Regular);
            password.Text = "Introdu-ti parola:";
        }
        private void loadPasswordTextBox()
        {
            TextBox password = new TextBox();
            password.Parent = this.main;
            password.Font = new Font("Microsoft Sitka Small", 12, FontStyle.Regular);
            password.Location = new Point(260, 150);
            password.Width = 340;
            password.UseSystemPasswordChar = true;
        }
        private void loadLogInButton()
        {
            Button login = new Button();
            login.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
            login.BackColor = Color.PaleGreen;
            login.Parent = this.main;
            login.Location = new Point(200, 230);
            login.Width = 315;
            login.Height = 120;
            login.Text = "Log in";
            login.Click += delegate (Object sender2, EventArgs e2) { service.login_Click(sender2, e2, this.main.Controls[1].Text, this.main.Controls[3].Text); };
        }
        public override void loadMain()
        {
            this.main.Controls.Clear();
            loadUsernameLabel();
            loadUsernameTextBox();
            loadPasswordLabel();
            loadPasswordTextBox();
            loadLogInButton();
        }

        protected override void loadFooter()
        {
            this.footer.Controls.Clear();
            Label trademark = new Label();
            trademark.Parent = footer;
            trademark.Location = new Point(355, 10);
            trademark.Width = 350;
            trademark.Height = 80;
            trademark.AutoSize = false;
            trademark.Text = "Cristi Sports Store, toate drepturile rezervate";
            trademark.TextAlign = ContentAlignment.BottomRight;
            trademark.Font = new Font("Microsoft Sant Serif", 12, FontStyle.Regular);
        }
    }
}
