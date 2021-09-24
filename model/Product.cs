using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Emag.model
{
    class Product
    {
        private int id;
        private string name;
        private double price;
        private int stock;
        private string description;
        private string imageUrl;
        private Image image;
        //private PictureBox pctBox;

        public Product(int id, string name, double price, int stock, string description, string imageUrl)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.stock = stock;
            this.description = description;
            this.imageUrl = imageUrl;
            string fullPath = Application.StartupPath + @"\pictures\" + this.imageUrl;
            if(this.id != -1)
                this.image = Image.FromFile(fullPath);
            //Console.WriteLine(this.imageUrl);
            //pctBox = new PictureBox();
            //pctBox.Image = Image.FromFile(this.imageUrl);
            
        }

        public int ID { get => this.id; set => this.id = value; }
        public string Name { get => this.name; set => this.name = value; }
        public double Price { get => this.price; set => this.price = value; }
        public int Stock { get => this.stock; set => this.stock = value; }
        public string Description { get => this.description; set => this.description = value; }
        public string ImageUrl { get => this.imageUrl; }
        public Image Image { get => this.image; set => this.image = value; }
        
        public override string ToString()
        {
            return this.id.ToString() + "|" + this.name + "|" + this.price.ToString() + "|" + this.stock.ToString() + "|" + this.description + "|" + imageUrl;
        }
        public bool Equals(Product product) { return this.id.Equals(product.ID); }
    }
}
