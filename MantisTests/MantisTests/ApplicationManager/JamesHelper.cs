using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalisticTelnet;

namespace MantisTests
{
    public class JamesHelper : HelperBase
    {
        public JamesHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Add(AccountData account)
        {
            if (Verify(account))
            {
                return;
            }

            var telnetConnection = LoginToJames();
            telnetConnection.WriteLine("adduser " + account.Name + " " + account.Password);
            Console.WriteLine(telnetConnection.Read());
        }

        public void Delete(AccountData account)
        {
            if (!Verify(account))
            {
                return;
            }

            var telnetConnection = LoginToJames();
            telnetConnection.WriteLine("deluser " + account.Name);
            Console.WriteLine(telnetConnection.Read());
        }

        public bool Verify(AccountData account)
        {
            var telnetConnection = LoginToJames();
            telnetConnection.WriteLine("verify " + account.Name);
            string response = telnetConnection.Read();
            Console.WriteLine(response);
            return !response.Contains("does not exist");
        }

        private TelnetConnection LoginToJames()
        {
            TelnetConnection telnetConnection = new TelnetConnection("localhost", 4555);
            Console.WriteLine(telnetConnection.Read());
            telnetConnection.WriteLine("root");
            Console.WriteLine(telnetConnection.Read());
            telnetConnection.WriteLine("root");
            Console.WriteLine(telnetConnection.Read());

            return telnetConnection;
        }
    }
}
