using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  AutoItX3Lib;

namespace AddressbookWebTestsAutoIt
{
    public class ApplicationManager
    {
        public static string WinTitle = "Free Address Book";

        private AutoItX3 aux;
        private GroupHelper groupHelper;

        public ApplicationManager()
        {
            aux = new AutoItX3();
            aux.Run(@"D:\FreeAddressBookPortable\AddressBook.exe", nShowFlag: aux.SW_SHOW);
            aux.WinWait(WinTitle);
            aux.WinActivate(WinTitle);
            aux.WinWaitActive(WinTitle);

            groupHelper = new GroupHelper(this);
        }

        public void Stop()
        {
            aux.ControlClick(WinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d510");
        }

        public AutoItX3 Aux
        {
            get
            {
                return aux;
            }
        }

        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
    }
}
