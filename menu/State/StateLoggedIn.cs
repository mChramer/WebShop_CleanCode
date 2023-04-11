using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.State
{
    internal class StateLoggedIn : IState
    {
        public void ExecuteMenuActionLogin(WebShop w)
        {
            w.currentState = new StateLoggedOut();
            w.currentCustomer = null;
            w.UpdateMenusWithCurrentState();
            w.currentMenu = w.mainMenu;
            w.currentChoice = 1;
        }

        public string GetMenuActionName()
        {
            return "Logout";
        }
    }
}
