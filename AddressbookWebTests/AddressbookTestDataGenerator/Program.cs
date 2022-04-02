using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressbookWebTests;

namespace AddressbookTestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int testDataCount = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            for (int i = 0; i < testDataCount; i++)
            {
                writer.WriteLine($"{TestBase.GenerateRandomString(10)}," +
                                 $"{TestBase.GenerateRandomString(10)}," +
                                 $"{TestBase.GenerateRandomString(10)}");
            }
            writer.Close();
        }
    }
}
