using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.State
{
    internal class StateLoggedOut : IState
    {
        public void ExecuteMenuActionLogin(WebShop w)
        {
            w.currentMenu = w.loginMenu;
            
        }

        public string GetMenuActionName()
        {
            return "Login";
        }
    }
}
