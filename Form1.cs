using Emag.control;
using Emag.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emag.services;

namespace Emag
{
    public partial class Form1 : Form
    {
        private Panel header = new Panel();
        private Panel main = new Panel();
        private Panel aside = new Panel();
        private Panel footer = new Panel();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Cristi Sports Store, welcome!";
            this.MaximumSize = new Size(731, 634);
            this.MinimumSize = new Size(731, 634);
            this.SetDesktopLocation(225, 20);
            ViewProducts view = new ViewProducts(header, main, aside, footer, 1, this, 0);
        }
    }
}
