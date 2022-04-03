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
using Excel = Microsoft.Office.Interop.Excel;
using Formatting = Newtonsoft.Json.Formatting;

namespace AddressbookTestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int testDataCount = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = args[2].Split('.')[1];

            List<GroupData> groups = new List<GroupData>();
            List<ContactData> contacts = new List<ContactData>();

            if (dataType == "groups")
            {
                groups = GenerateRandomGroups(testDataCount);
                if (format == "excel")
                {
                    WriteGroupsToExcelFile(groups, fileName);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(fileName);
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
                        Console.Out.WriteLine("Unknown File Format");
                    }

                    writer.Close();
                }
            }
            else if (dataType == "contacts")
            {
                contacts = GenerateRandomContacts(testDataCount);
                StreamWriter writer = new StreamWriter(fileName);
                if (format == "xml")
                {
                    WriteContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    WriteContactsToJsonFile(contacts, writer);
                }
                else
                {
                    Console.Out.WriteLine("Unknown File Format");
                }
                writer.Close();
            }
        }

        static List<GroupData> GenerateRandomGroups(int testDataCount)
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < testDataCount; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }

            return groups;
        }

        static List<ContactData> GenerateRandomContacts(int testDataCount)
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < testDataCount; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(20),
                    TestBase.GenerateRandomString(20),
                    TestBase.GenerateRandomString(20))
                {
                    Address = TestBase.GenerateRandomString(10),
                    Company = TestBase.GenerateRandomString(10),
                    Email2 = TestBase.GenerateRandomString(10),
                    Email3 = TestBase.GenerateRandomString(10)
                });
            }

            return contacts;
        }

        static void WriteGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            int row = 1;
            foreach (var group in groups)
            {
                worksheet.Cells[row, 1] = group.Name;
                worksheet.Cells[row, 2] = group.Header;
                worksheet.Cells[row, 3] = group.Footer;
                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            workbook.SaveAs(fullPath);

            workbook.Close();
            app.Quit();
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

        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Formatting.Indented));
        }
    }
}
