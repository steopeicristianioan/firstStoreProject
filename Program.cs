using Emag.control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emag
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /*ControlProducts products = new ControlProducts();
            Console.WriteLine(products.getById(1).ToString());*/

            /*ControlOrders orders = new ControlOrders();
            orders.logOrders();*/
            
            

            
        }
    }
}
