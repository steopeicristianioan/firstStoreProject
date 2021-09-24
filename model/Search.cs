using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emag.model
{
    class Search
    {
        private string name;
        private double lowerPrice;
        private double upperPrice;
        private List<Product> forSearching;

        public Search(string name, double lower, double upper, List<Product> forSearching)
        {
            this.name = name;
            this.lowerPrice = lower;
            this.upperPrice = upper;
            this.forSearching = forSearching;
        }

        public bool isOkProduct(Product product)
        {
            if (this.name.Equals(string.Empty) && (product.Price >= this.lowerPrice && product.Price <= this.upperPrice))
                return true;
            return product.Name.Contains(this.name) && (product.Price >= this.lowerPrice && product.Price <= this.upperPrice);
        }
        public List<Product> performSearch()
        {
            if (name.Equals(string.Empty) && lowerPrice.Equals(-1) && upperPrice.Equals(-1))
                return this.forSearching;
            List<Product> found = new List<Product>();
            foreach(Product product in this.forSearching)
            {
                if (isOkProduct(product))
                    found.Add(product);
            }
            return found;
        }
    }
}
