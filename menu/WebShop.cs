using menu.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace menu
{
    public class WebShop
    {
        #region Singleton Pattern
        private static WebShop instance = null;

        public static WebShop GetInstance()
        {
            if (instance == null)
                instance = new WebShop();
            
            return instance;
        }
        private WebShop()
        {
            this.products = database.GetProducts();
            this.customers = database.GetCustomers();
            this.currentState = new StateLoggedOut();

            UpdateMenusWithCurrentState();
        }
        #endregion

        public Menu currentMenu = null;
        bool isRunning = true;
        public int currentChoice = 1;
        public int numberOfButtons = 1;
        public IState currentState;

        public Menu mainMenu = null;
        public Menu waresMenu = null;
        public Menu sortMenu = null;
        public Menu loginMenu = null;
        public Menu customerMenu = null;
        public Menu purchaseWaresMenu = null;
        public Customer currentCustomer { get; set; }

        public List<Product> products = new List<Product>();
        public List<Customer> customers = new List<Customer>();

        Database database = new Database();

        public void Run()
        {

            currentMenu = mainMenu;

            // Genom att använda currentMenu, definierar jag beteendet som kommer gälla för samtliga menyer.
            while (isRunning)
            {
                numberOfButtons = currentMenu.Options.Count;

                PrintMenuAndButtonsAndCursor();

                string input = Console.ReadLine().ToLower();
                ProcessInput(input);
            }
        }

        public void UpdateMenusWithCurrentState()
        {
            this.mainMenu = Menu.GenerateMainMenu(this.currentState);
            this.waresMenu = Menu.GenerateWaresMenu(this.currentState);
            this.sortMenu = Menu.GenerateSortMenu();
            this.loginMenu = Menu.GenerateLoginMenu();
            this.customerMenu = Menu.GenerateCustomerMenu();
            this.purchaseWaresMenu = Menu.GeneratePurchaseWaresMenu(this);
        }

        private void ProcessInput(string input)
        {
            switch (input)
            {
                case "l":
                case "left":
                    if (currentChoice == 1) return; // eller skriv ut lämplig kommentar
                    currentChoice--;
                    break;
                case "r":
                case "right":
                    if (currentChoice == numberOfButtons) return; // eller skriv ut lämplig kommentar
                    currentChoice++;
                    break;
                case "q":
                case "quit":
                    Environment.Exit(0);
                    break;
                case "b":
                case "back":
                    if (currentMenu == mainMenu)
                        Console.WriteLine("You are already on the main menu.\n");
                    else
                        currentMenu = mainMenu;
                    break;
                case "o":
                case "k":
                case "ok":
                    currentMenu.Options[currentChoice - 1].ExecuteAction(this);
                    break;
                default:
                    Console.WriteLine("That is not an applicable option.\n");
                    break;
            }

        }

        private void PrintMenuAndButtonsAndCursor()
        {
            // Skriv ut förfrågan
            Console.WriteLine(currentMenu.PromptQuestion);


            // Skriv ut de olika options
            int index = 1;
            foreach (MenuOption o in currentMenu.Options)
                Console.WriteLine($"{index++}. {o.Name}");


            // Skriv ut 1,2,3,4 osv..
            string buttons = string.Empty;
            for (int i = 1; i <= numberOfButtons; i++) buttons += $"{i}\t";
            Console.WriteLine(buttons);

            // Skriv ut en cursor (currentChoice)
            string cursor = string.Empty;
            for (int i = 1; i < currentChoice; i++) cursor += "\t";
            Console.WriteLine(cursor + "|");

            // Skriv ut en tom rad
            Console.WriteLine(string.Empty);

            if (currentMenu == purchaseWaresMenu)
                Console.WriteLine($"Your funds: {currentCustomer.Funds}kr");

            Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
        }
    }
}
