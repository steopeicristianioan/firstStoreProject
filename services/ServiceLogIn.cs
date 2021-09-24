using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Emag.view;
using Emag.model;
using Emag.control;

namespace Emag.services
{
    class ServiceLogIn
    {
        private ViewLogIn view;
        public ServiceLogIn(ViewLogIn view)
        {
            this.view = view;
        }
        
        public void login_Click(Object snder, EventArgs e, string name, string password)
        {
            ControlCustomers customers = new ControlCustomers();
            int id = customers.attemptLogIn(name, password);
            if (id.Equals(-1))
                MessageBox.Show("Nu am putut gasi contul, mai verifica datele inca o data!");
            else
            {
                ViewProducts products = new ViewProducts(this.view.Header, this.view.Main, this.view.Aside, this.view.Footer, id, this.view.Form, 1);
            }
        }
    }
}
