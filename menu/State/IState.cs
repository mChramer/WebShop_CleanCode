using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.State
{
    public interface IState
    {
        public string GetMenuActionName();

        public void ExecuteMenuActionLogin(WebShop w);
        
    }
}
