using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using AddressbookWebTests;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace AddressbookTestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int testDataCount = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[2];

            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < testDataCount; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }

            if (format == "csv")
            {
                WriteGroupsToCsvFile(groups, writer);
            }
            else if (format == "xml")
            {
                WriteGroupsToXmlFile(groups, writer);
            }
            else if (format == "json")
            {
                WriteGroupsToJsonFile(groups, writer);
            }
            else
            {
                Console.Out.WriteLine("Это фаил!!!! ФАИИИЛЛЛ!!!!!!!!!!");
            }

            writer.Close();
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (var group in groups)
            {
                writer.WriteLine($"{group.Name}," +
                                 $"{group.Header}," +
                                 $"{group.Footer}");
            }
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
        }
    }
}
