using menu.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using menu.State;

namespace menu
{
    public class Customer
    {
        public string Password;
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Funds { get; set; }
        public List<customerOrder> CustomerOrders { get; set; }
        public Customer(string username, string password, string firstName, string lastName, string email, int age, string address, string phoneNumber)
        {
            Username = username;
            this.Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Address = address;
            PhoneNumber = phoneNumber;
            CustomerOrders = new List<customerOrder>();
            Funds = 0;
        }

        public bool CanAfford(int price)
        {
            return Funds >= price;
        }
        public bool CheckPassword(string password)
        {
            if (password == null)
            {
                return true;
            }
            return password.Equals(this.Password);
        }
        public void PrintCustomerInfo()
        {
            Console.Write($"\nUsername: {Username}");
            if (Password != null)
            {
                Console.Write($", Password: {Password}");
            }
            if (FirstName != null)
            {
                Console.Write($", First Name: {FirstName}");
            }
            if (LastName != null)
            {
                Console.Write($", Last Name: {LastName}");
            }
            if (Email != null)
            {
                Console.Write($", Email: {Email}");
            }
            if (Age != -1)
            {
                Console.Write($", Age: {Age}");
            }
            if (Address != null)
            {
                Console.Write($", Address: {Address}");
            }
            if (PhoneNumber != null)
            {
                Console.Write($", Phone Number: {PhoneNumber}");
            }
            Console.WriteLine($", Funds: {Funds}\n");
        }
        public void PrintOrders()
        {
            Console.WriteLine();
            foreach (customerOrder order in CustomerOrders)
            {
                order.PrintInfo();
            }
            Console.WriteLine();
        }
        public static void AddFunds(WebShop w)
        {
            Console.WriteLine("How many funds would you like to add?");
            string amountString = Console.ReadLine();

            if (int.TryParse(amountString, out int amount))
            {
                if (amount < 0)
                {
                    Console.WriteLine("\nDon't add negative amounts.\n");
                }
                else
                {
                    w.currentCustomer.Funds += amount;
                    Console.WriteLine($"\n{amount} added to your profile.\n");
                }
            }
            else
            {
                Console.WriteLine("\nPlease write a number next time.\n");
            }

        }


        public static void InitNewCustomer(WebShop w)
        {
            string username = null;
            string password = null;
            string firstName = null;
            string lastName = null;
            string email = null;
            string address = null;
            string phoneNumber = null;
            int age = -1;

            Console.WriteLine("Please write your username.");
            username = Console.ReadLine();

            //kontrollerar om det finns en customer som redan har det användarnamnet
            bool customerExists = w.customers.Any(c => c.Username == username);
            while (customerExists)
            {
                Console.WriteLine("Username already exists.");
                username = Console.ReadLine();
                customerExists = w.customers.Any(c => c.Username == username);
            }

            Console.WriteLine("Do you want a password? y/n?");
            string response = Console.ReadLine();
            while(response?.ToLower() != "y" && response?.ToLower() != "n")
            {
                Console.WriteLine("Do you want a password? y/n?");
                response = Console.ReadLine();
            }
            if (response == "y")
            {
                Console.WriteLine("Please input your password.");
                password = Console.ReadLine();
            }

            Console.WriteLine("Do you want a first name? y/n?");
            response = Console.ReadLine();
            while(response?.ToLower() != "y" && response?.ToLower() != "n")
            {
                Console.WriteLine("Do you want a first name? y/n?");
                response = Console.ReadLine();
            }
            if (response == "y")
            {
                Console.WriteLine("Please input your first name.");
                firstName = Console.ReadLine();
            }

            Console.WriteLine("Do you want a last name? y/n?");
            response = Console.ReadLine();
            while (response?.ToLower() != "y" && response?.ToLower() != "n")
            {
                Console.WriteLine("Do you want a last name? y/n?");
                response = Console.ReadLine();
            }
            if (response == "y")
            {
                Console.WriteLine("Please input your last name.");
                lastName= Console.ReadLine();
            }

            Console.WriteLine("Do you want an email? y/n?");
            response = Console.ReadLine();
            while (response?.ToLower() != "y" && response?.ToLower() != "n")
            {
                Console.WriteLine("Do you want an email? y/n?");
                response = Console.ReadLine();
            }
            if (response == "y")
            {
                Console.WriteLine("Please input your email.");
                email = Console.ReadLine();
            }

            Console.WriteLine("Do you want an age? y/n?");
            response = Console.ReadLine();
            while (response?.ToLower() != "y" && response?.ToLower() != "n")
            {
                Console.WriteLine("Do you want an age? y/n?");
                response = Console.ReadLine();
            }
            if (response == "y")
            {
                Console.WriteLine("Please write your age.");
                string ageString = Console.ReadLine();
                try
                {
                    age = int.Parse(ageString);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\nPlease write a number.\n");
                }
            
            }
            Console.WriteLine("Do you want an adress? y/n?");
            response = Console.ReadLine();
            while (response?.ToLower() != "y" && response?.ToLower() != "n")
            {
                Console.WriteLine("Do you want an adress? y/n?");
                response = Console.ReadLine();
            }
            if (response == "y")
            {
                Console.WriteLine("Please write your adress.");
                address= Console.ReadLine();
            }

            Console.WriteLine("Do you want a phone number? y/n?");
            response = Console.ReadLine();
            while (response?.ToLower() != "y" && response?.ToLower() != "n")
            {
                Console.WriteLine("Do you want a phone number? y/n?");
                response = Console.ReadLine();
            }
            if (response == "y")
            {
                Console.WriteLine("Please input your phone number.");
                phoneNumber = Console.ReadLine();
            }

            Customer newCustomer = new Customer(username, password, firstName, lastName, email, age, address, phoneNumber);
            w.customers.Add(newCustomer);
            w.currentCustomer = newCustomer;
            Console.WriteLine($"\n{newCustomer.Username} successfully added and is now logged in.\n");
            w.currentState = new StateLoggedIn();
            w.UpdateMenusWithCurrentState();
        }

       

        

       





    }
}
