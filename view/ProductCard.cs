using Emag.control;
using Emag.model;
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
    class ProductCard
    {
        private Panel card;
        private Product product;
        private ViewProducts view;
        public ProductCard(Panel card, Product product, ViewProducts view)
        {
            this.view = view;
            this.card = card;
            this.product = product;
            this.card.Parent = this.view.Main;
            this.card.Width = 150;
            this.card.Height = 260;
            this.card.BorderStyle = BorderStyle.FixedSingle;
            this.card.BackColor = Color.PaleGreen;

            if (this.view.HasAccount == 1)
                this.card.Click += new EventHandler(this.card_Click);
        }
        public void setLoacation(int x, int y)
        {
            this.card.Location = new Point(x, y);
        }

        public void loadCard()
        {
            PictureBox box = new PictureBox();
            box.Parent = this.card;
            box.Location = new Point(10, 10);
            box.Width = 130;
            box.Height = 120;
            box.Image = this.product.Image;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.Click += new EventHandler(this.card_Click);

            Label name = new Label();
            name.Parent = this.card;
            name.Location = new Point(10, 140);
            name.Width = 130;
            name.Height = 50;
            bool inStock = this.product.Stock > 0 ? true : false;
            name.Text = this.product.Name + "\nDisponibilitate: " + (inStock ? "In stoc" : "Indisponibil");
            name.TextAlign = ContentAlignment.MiddleCenter;
            name.Click += new EventHandler(this.card_Click);

            Label price = new Label();
            price.Parent = this.card;
            price.Location = new Point(10, 200);
            price.Width = 130;
            price.Height = 50;
            price.Text = this.product.Price.ToString() + "$";
            price.TextAlign = ContentAlignment.MiddleCenter;
            price.BorderStyle = BorderStyle.FixedSingle;
            price.BackColor = Color.PaleTurquoise;
            price.Click += new EventHandler(this.card_Click);

            foreach (Control control in this.card.Controls)
                control.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
        }
        private void card_Click(Object sender, EventArgs e)
        {
            ServiceHome service = new ServiceHome(this.view);
            service.loadProductDetails(this.product);
        }
    }
}
