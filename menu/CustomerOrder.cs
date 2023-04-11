using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu
{
    public class customerOrder
    {
        public string Name { get; set; }
        public int BoughtFor { get; set; }
        public DateTime PurchaseTime { get; set; }
        public customerOrder(string name, int boughtFor, DateTime purchaseTime)
        {
            Name = name;
            BoughtFor = boughtFor;
            PurchaseTime = purchaseTime;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"{Name}: {BoughtFor} kr, {PurchaseTime} in stock.");
        }
    }
}
