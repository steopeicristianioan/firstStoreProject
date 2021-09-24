using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Emag.model;
using Emag.view;
using Emag.services;

namespace Emag.view
{
    class OrderCard
    {
        private Panel card;
        private ViewProfile view;
        private Orders order;

        public OrderCard(Panel card, ViewProfile view, Orders order)
        {
            this.card = card;
            this.view = view;
            this.order = order;

            this.card.Parent = this.view.Main;
            this.card.BorderStyle = BorderStyle.FixedSingle;
        }

        public void setLocation(int x, int y)
        {
            this.card.Location = new Point(x, y);
        }
        public void loadCard()
        {
            this.card.Width = 150;
            this.card.Height = 150;

            this.card.BackColor = Color.PaleTurquoise;

            Label id = new Label();
            id.Parent = this.card;
            id.Location = new Point(5, 15);
            id.Width = 140;
            id.Height = 20;
            id.TextAlign = ContentAlignment.MiddleCenter;
            id.Text = "Comanda #" + order.ID;

            Label ammount = new Label();
            ammount.Parent = this.card;
            ammount.Location = new Point(10, 35);
            ammount.Width = 130;
            ammount.Height = 50;
            ammount.TextAlign = ContentAlignment.MiddleCenter;
            ammount.Text = "Produse distincte: " + order.Ammount;

            ServiceProfile service = new ServiceProfile(this.view);

            Button details = new Button();
            details.BackColor = Color.PaleGreen;
            details.Parent = this.card;
            details.Location = new Point(10, 85);
            details.Width = 130;
            details.Height = 50;
            details.Text = "Click pentru detalii";
            details.Click += delegate (Object sender2, EventArgs e2) { service.details_Click(sender2, e2, order); };
        }
    }
}
