using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int NrInStock { get; set; }
        public Product(string name, int price, int nrInStock)
        {
            Name = name;
            Price = price;
            NrInStock = nrInStock;
        }
        public bool InStock()
        {
            return NrInStock > 0;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"{Name}: {Price} kr, {NrInStock} in stock.");
        }

        public static void PrintWaresList(WebShop w)
        {
            foreach (Product product in w.products)
            {
                product.PrintInfo();
            }
            Console.WriteLine();
        }
    }
}
