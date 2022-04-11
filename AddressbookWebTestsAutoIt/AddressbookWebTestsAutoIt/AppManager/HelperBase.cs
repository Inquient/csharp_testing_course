using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace AddressbookWebTestsAutoIt
{
    public class HelperBase
    {
        protected ApplicationManager manager;
        protected string WinTitle;
        protected AutoItX3 aux;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            WinTitle = ApplicationManager.WinTitle;
            aux = manager.Aux;
        }
    }
}
