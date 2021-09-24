using Emag.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Emag.control
{
    class ControlCustomers
    {
        private List<Customer> allCustomers;
        private string url = Application.StartupPath + "/customers.txt";
        public ControlCustomers()
        {
            this.allCustomers = new List<Customer>();
            read();
        }

        public void read()
        {
            this.allCustomers.Clear();
            StreamReader reader = new StreamReader(url);
            string line = string.Empty;
            while((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split('|');
                Customer customer = new Customer(int.Parse(values[0]), values[1], values[2], values[3], values[4]);
                this.allCustomers.Add(customer);
            }
            reader.Close();
        }
        public void write()
        {
            StreamWriter writer = new StreamWriter(url);
            string result = string.Empty;
            foreach(Customer customer in allCustomers)
            {
                result += customer.ToString();
                result += "\n";
            }
            writer.Write(result);
            writer.Close();
        }
        public void logCustomers()
        {
            foreach(Customer customer in allCustomers)
                Console.WriteLine(customer.ToString());
        }

        public Customer getById(int id)
        {
            Customer notFound = new Customer(-1, "", "", "", "");
            foreach (Customer customer in allCustomers)
                if (customer.ID.Equals(id))
                    return customer;
            return notFound;
        }
        public int idByName(string name)
        {
            foreach (Customer customer in allCustomers)
                if (customer.Username.Equals(name))
                    return customer.ID;
            return -1;
        }
        public int attemptLogIn(string name, string password)
        {
            int id = this.idByName(name);
            if (id.Equals(-1)) return -1;
            Customer customer = this.getById(id);
            if (customer.Password.Equals(password))
                return id;
            return -1;
        }
    }
}
