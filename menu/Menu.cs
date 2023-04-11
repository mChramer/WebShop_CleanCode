using menu.State;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu
{
    public class Menu
    {
        // private set, för att man bara ska kunna sätta ett värde inuti klassen
        public string Name { get; private set; }
        public string PromptQuestion { get; private set; }
        public List<MenuOption> Options { get; set; }

        public Menu(string aName, string aPromptQuestion, List<MenuOption> aOptions)
        {
            this.Name = aName;
            this.PromptQuestion = aPromptQuestion;
            this.Options = aOptions;
        }

        public static Menu GenerateCustomerMenu()
        {

            List<MenuOption> options = new List<MenuOption>();

            MenuOption.ExecuteActionDelegate ShowInloggedCustomerOrders = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                w.currentCustomer.PrintOrders();

            });
            options.Add(new MenuOption("See your orders", ShowInloggedCustomerOrders));

            MenuOption.ExecuteActionDelegate SetInloggedCustomerInfo = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                w.currentCustomer.PrintCustomerInfo();
            });
            options.Add(new MenuOption("See your info", SetInloggedCustomerInfo));

            MenuOption.ExecuteActionDelegate AddFunds = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Customer.AddFunds(w);
                w.currentChoice = 1;
            });
            options.Add(new MenuOption("Add funds", AddFunds));

            Menu result = new Menu("customer menu", "What would you like to do?", options);

            return result;
        }

        public static Menu GenerateLoginMenu()

        {
            string givenPassword = null;
            string givenUsername = null;

            List<MenuOption> options = new List<MenuOption>();

            MenuOption.ExecuteActionDelegate SetUsername = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Console.WriteLine("A keyboard appears.\nPlease input your username");
                givenUsername = Console.ReadLine();

            });
            options.Add(new MenuOption("Set username", SetUsername));

            MenuOption.ExecuteActionDelegate SetPassword = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Console.WriteLine("A keyboard apperas.\nPlease input your password");
                givenPassword = Console.ReadLine();
            });
            options.Add(new MenuOption("Set password", SetPassword));

            MenuOption.ExecuteActionDelegate LoginCustomer = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Customer loggedinCustomer = w.customers.Where(c => c.Username == givenUsername && c.Password == givenPassword).FirstOrDefault();
                if (loggedinCustomer != null)
                {
                    w.currentCustomer = loggedinCustomer;
                    w.currentState = new StateLoggedIn();
                    w.UpdateMenusWithCurrentState();
                    w.currentMenu = w.mainMenu;
                    w.currentChoice = 1;
                    Console.WriteLine($"{w.currentCustomer.Username} logged in");
                }
                else { Console.WriteLine("Incomplete data."); }
            });
            options.Add(new MenuOption("Login", LoginCustomer));

            MenuOption.ExecuteActionDelegate RegisterCustomer = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Customer.InitNewCustomer(w);
                w.currentMenu = w.mainMenu;
                w.currentChoice = 1;
            });
            options.Add(new MenuOption("Register", RegisterCustomer));


            Menu result = new Menu("login menu", "Please submit username & password", options);

            return result;
        }



        public static Menu GenerateSortMenu()
        {
            List<MenuOption> options = new List<MenuOption>();

            MenuOption.ExecuteActionDelegate SortByNameAsc = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Console.WriteLine("\nSorted by name in ascending order:");
                SortingAlgorithm.QuickSort(w.products, "name", true); SortingAlgorithm.PrintProductsList(w.products);
                Console.WriteLine();
            });
            options.Add(new MenuOption("Sort by name asc.", SortByNameAsc));


            MenuOption.ExecuteActionDelegate SortByNameDesc = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Console.WriteLine("\nSorted by name in descending order:");
                SortingAlgorithm.QuickSort(w.products, "name", false); SortingAlgorithm.PrintProductsList(w.products);
                Console.WriteLine();
            });
            options.Add(new MenuOption("Sort by name desc.", SortByNameDesc));


            MenuOption.ExecuteActionDelegate SortByPriceAsc = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Console.WriteLine("\nSorted by price in ascending order:");
                SortingAlgorithm.QuickSort(w.products, "price", true); SortingAlgorithm.PrintProductsList(w.products);
                Console.WriteLine();
            });
            options.Add(new MenuOption("Sort by price asc.", SortByPriceAsc));


            MenuOption.ExecuteActionDelegate SortByPriceDesc = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Console.WriteLine("\nSorted by price in descending order:");
                SortingAlgorithm.QuickSort(w.products, "price", false); SortingAlgorithm.PrintProductsList(w.products);
                Console.WriteLine();
            });
            options.Add(new MenuOption("Sort by price desc.", SortByPriceDesc));

            Menu result = new Menu("sort menu", "How would you like to sort them?", options);

            return result;
        }

        public static Menu GenerateWaresMenu(IState aState)
        {
            List<MenuOption> options = new List<MenuOption>();

            MenuOption.ExecuteActionDelegate PrintAllWares = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                Product.PrintWaresList(w);
            });
            options.Add(new MenuOption("See all wares", PrintAllWares));


            MenuOption.ExecuteActionDelegate PurchaseWares = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                w.currentMenu = w.purchaseWaresMenu;
                w.currentChoice = 1;

            });
            options.Add(new MenuOption("Purchase a ware", PurchaseWares));


            MenuOption.ExecuteActionDelegate GoToSortMenu = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                w.currentMenu = w.sortMenu;
                w.currentChoice = 1;
            });
            options.Add(new MenuOption("Sort wares", GoToSortMenu));

            MenuOption.ExecuteActionDelegate Login = new MenuOption.ExecuteActionDelegate((WebShop w) =>
            {
                aState.ExecuteMenuActionLogin(w);
                w.UpdateMenusWithCurrentState();
                w.currentChoice = 1;
            });
            options.Add(new MenuOption(aState.GetMenuActionName(), Login));

            Menu result = new Menu("wares menu", "What would you like to do?", options);

            return result;
        }

        public static Menu GenerateMainMenu(IState aState)
        {
            List<MenuOption> options = new List<MenuOption>();

            MenuOption.ExecuteActionDelegate GoToSeeWaresMenu =
                new MenuOption.ExecuteActionDelegate((WebShop w) =>
                {
                    w.currentMenu = w.waresMenu;
                    w.currentChoice = 1;
                });
            options.Add(new MenuOption("See Wares", GoToSeeWaresMenu));

            MenuOption.ExecuteActionDelegate GoToCustomerInfoMenu =
                new MenuOption.ExecuteActionDelegate((WebShop w) =>
                {
                    w.currentMenu = w.customerMenu;
                    w.currentChoice = 1;
                });
            options.Add(new MenuOption("Customer Info", GoToCustomerInfoMenu));

            MenuOption.ExecuteActionDelegate GoToLoginMenu =
                new MenuOption.ExecuteActionDelegate((WebShop w) =>
                {
                    aState.ExecuteMenuActionLogin(w);
                    w.UpdateMenusWithCurrentState();
                    w.currentChoice = 1;
                });
            options.Add(new MenuOption(aState.GetMenuActionName(), GoToLoginMenu));

            Menu result = new Menu("main menu", "What would you like to do?", options);


            return result;
        }

        public static Menu GeneratePurchaseWaresMenu(WebShop w)
        {
            List<MenuOption> options = new List<MenuOption>();

            foreach (Product product in w.products)
            {
                MenuOption.ExecuteActionDelegate BuyProduct =
                    new MenuOption.ExecuteActionDelegate((WebShop w) =>
                    {
                        if (product.InStock())
                        {
                            if (w.currentCustomer.CanAfford(product.Price))
                            {
                                w.currentCustomer.Funds -= product.Price;
                                product.NrInStock--;
                                w.currentCustomer.CustomerOrders.Add(new customerOrder(product.Name, product.Price, DateTime.Now));
                                Console.WriteLine($"\nSuccessfully bought {product.Name}\n");
                                w.UpdateMenusWithCurrentState();
                                w.currentMenu = w.purchaseWaresMenu;
                                w.currentChoice = 1;
                            }
                            else
                            {
                                Console.WriteLine("\nYou cannot afford.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nNot in stock\n");
                        }

                    });
                options.Add(new MenuOption($"{product.Name}: {product.Price} kr, {product.NrInStock} in stock.", BuyProduct));
            }

            Menu result = new Menu("Purchase a ware", "What would you like to purchase?", options);

            return result;
        }

    }
}

