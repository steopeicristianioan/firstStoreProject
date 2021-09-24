using Emag.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Emag.view;

namespace Emag.control
{
    class ControlProducts
    {
        private ViewProducts view;
        private List<Product> allProducts;
        private string url = Application.StartupPath + "/products.txt";
        public ControlProducts()
        {
            this.allProducts = new List<Product>();
            read();
        }
        public ControlProducts(ViewProducts view)
        {
            this.view = view;
            this.allProducts = new List<Product>();
            read();
        }

        public void read()
        {
            this.allProducts.Clear();
            StreamReader reader = new StreamReader(url);
            string line = string.Empty;
            while((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split('|');
                Product product = new Product(int.Parse(values[0]), values[1], double.Parse(values[2]), int.Parse(values[3]), values[4], values[5]);
                allProducts.Add(product);
            }
            reader.Close();
        }
        public void write()
        {
            StreamWriter writer = new StreamWriter(url);
            string result = string.Empty;
            foreach(Product product in allProducts)
            {
                result += product.ToString();
                result += "\n";
            }
            writer.Write(result);
            writer.Close();
        }
        public void logProducts()
        {
            foreach(Product product in allProducts)
            {
                Console.WriteLine(product.ToString());
            }
        }

        public Product getById(int id)
        {
            Product notFound = new Product(-1, "", 0.0, 0, "", "");
            foreach(Product product in allProducts)
            {
                if (product.ID.Equals(id))
                    return product;
            }
            return notFound;
        }
        public void loadAllProducts()
        {
            this.loadProducts("", -1, -1);
        }
        public void loadProducts(string name, double price1, double price2)
        {
            this.view.Main.Controls.Clear();
            Search search = new Search(name, price1, price2, this.allProducts);
            List<Product> products = search.performSearch();
            if (products.Count.Equals(0))
            {
                Label error = new Label();
                error.Parent = this.view.Main;
                error.Location = new Point(10, 10);
                error.Width = 490;
                error.Height = 365;
                error.BorderStyle = BorderStyle.FixedSingle;
                error.Text = "Nu am gasit niciun produs care sa indeplineasca filtrele tale sau filtrele tale sunt invalide";
                error.TextAlign = ContentAlignment.MiddleCenter;
                error.Font = new Font("Microsoft San Serif", 14, FontStyle.Regular);
                return;
            }
            int x = 10, y = 10;
            int count = 0;
            foreach (Product product in products)
            {
                count++;
                Panel cardPanel = new Panel();
                ProductCard card = new ProductCard(cardPanel, product, this.view);
                card.setLoacation(x, y);
                card.loadCard();
                if (count % 3 == 0)
                {
                    x = 10;
                    y += 270;
                }
                else x += 160;
            }
        }

        
    }
}
