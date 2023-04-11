using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu
{
    public class MenuOption
    {
        public string Name { get; set; }

        public delegate void ExecuteActionDelegate(WebShop w);

        public ExecuteActionDelegate ExecuteAction;

        public MenuOption(string aName, ExecuteActionDelegate aAction = null)
        {
            this.Name = aName;
            this.ExecuteAction = aAction;
        }
    }

}
