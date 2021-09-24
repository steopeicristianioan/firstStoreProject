using Emag.model;
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
    class CartProductCard
    {
        private OrderDetails detail;
        private ViewCart view;
        private ViewProfile profile;
        private Panel card;

        private ControlProducts products;

        private int forCart = 1;
        public int ForCart { get => this.forCart; set => this.forCart = value; }

        public CartProductCard(OrderDetails detail, Panel card, ViewCart view)
        {
            this.view = view;
            this.detail = detail;
            this.card = card;
            this.card.Parent = this.view.Main;
            products = new ControlProducts();
        }
        public CartProductCard(OrderDetails detail, Panel card, ViewProfile profile)
        {
            this.detail = detail;
            this.card = card;
            this.profile = profile;
            this.card.Parent = this.profile.Main;
            products = new ControlProducts();
        }
        public void setLocation(int x, int y)
        {
            this.card.Location = new Point(x, y);
        }
        public void loadCard()
        {
            card.BorderStyle = BorderStyle.FixedSingle;
            card.BackColor = Color.PaleTurquoise;
            if (forCart == 1)
                card.Width = 585;
            else card.Width = 490;
            card.Height = 80;

            Product product = products.getById(this.detail.ProductId);

            PictureBox box = new PictureBox();
            box.Parent = this.card;
            box.Location = new Point(10, 10);
            box.Width = 60;
            box.Height = 60;
            box.Image = product.Image;
            box.SizeMode = PictureBoxSizeMode.StretchImage;

            Label name = new Label();
            name.Parent = this.card;
            name.Location = new Point(80, 10);
            name.Width = 150;
            name.Height = 60;
            name.Text = product.Name;
            name.TextAlign = ContentAlignment.MiddleCenter;

            Label price = new Label();
            price.Parent = this.card;
            price.Location = new Point(240, 10);
            price.Width = 120;
            price.Height = 60;
            price.Text = "Pret/buc: " + this.products.getById(detail.ProductId).Price.ToString() + " $";
            price.TextAlign = ContentAlignment.MiddleCenter;

            ServiceCart service = new ServiceCart(this.view);

            Label quantity = new Label();
            quantity.Parent = this.card;
            if (forCart == 1)
            {
                quantity.Location = new Point(410, 10);
                quantity.Width = 30;
                quantity.Text = this.detail.Quantity.ToString();
            }
            else
            {
                quantity.Location = new Point(360, 10);
                quantity.Width = 120;
                quantity.Text = this.detail.Quantity.ToString() + " bucat";
                if (this.detail.Quantity.Equals(1))
                    quantity.Text += "a";
                else quantity.Text += "i";
            }
            quantity.Height = 60;
            quantity.TextAlign = ContentAlignment.MiddleCenter;

            if(forCart == 1)
            {
                Button minus = new Button();
                minus.BackColor = Color.PaleGreen;
                minus.Parent = this.card;
                minus.Location = new Point(380, 25);
                minus.Width = 30;
                minus.Height = 30;
                minus.Text = "-";
                minus.Click += delegate (Object sender2, EventArgs e2) { service.minusButton_Click(sender2, e2, this.detail.ID, quantity); };

                Button plus = new Button();
                plus.BackColor = Color.PaleGreen;
                plus.Parent = this.card;
                plus.Location = new Point(440, 25);
                plus.Width = 30;
                plus.Height = 30;
                plus.Text = "+";
                plus.Click += delegate (Object sender2, EventArgs e2) { service.plusButton_Click(sender2, e2, this.detail.ID, quantity); };

                Button remove = new Button();
                remove.BackColor = Color.PaleGreen;
                remove.Parent = this.card;
                remove.Location = new Point(490, 25);
                remove.Width = 80;
                remove.Height = 30;
                remove.Text = "Remove";
                remove.Click += delegate (Object sender2, EventArgs e2) { service.remove_Click(sender2, e2, this.detail.ID); };
            }
        }

    }
}
