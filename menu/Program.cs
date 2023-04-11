using System;

namespace menu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the WebShop\n");

            WebShop w = WebShop.GetInstance();
            w.Run();
        }
    }
}