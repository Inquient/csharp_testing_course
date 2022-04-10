using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace AddressbookWebTests
{
    public class ContactHelper : HelperBase
    {
        private List<ContactData> contactsCache = null;

        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public List<ContactData> GetContactsList()
        {
            if(contactsCache == null)
            {
                contactsCache = new List<ContactData>();

                manager.Navigator.GoToHomePage();

                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry")); // Выбирает все элементы с именем entry
                foreach (IWebElement element in elements)
                {
                    var e = element.FindElements(By.CssSelector("td")); // Внутри элемента entry ищем все элементы td
                    var firstName = e[2].Text;
                    var lastName = e[1].Text;
                    string allEmails = e[4].Text;
                    contactsCache.Add(new ContactData(firstName, lastName)
                    {
                        AllEmails = allEmails
                    });
                }
            }
            
            return new List<ContactData>(contactsCache);
        }

        public int GetContactsCount()
        {
            manager.Navigator.GoToHomePage();
            return driver.FindElements(By.Name("entry")).Count;
        }

        public ContactHelper Create(ContactData contact)
        {
            GoToContactCreationPage();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();

            return this;
        }

        public ContactHelper CreateContactWithAdditionalFields(ContactData contact)
        {
            GoToContactCreationPage();
            FillContactForm(contact);
            FillAdditonalFields();
            SubmitContactCreation();
            ReturnToHomePage();

            return this;
        }

        public void CreateContactIfDoesNotExists(
            string contactFirstName = "contactToRemoveFirstName",
            string contactLastName = "contactToRemoveLastName")
        {
            manager.Navigator.GoToHomePage();
            if (!DoesAnyContactExist())
            {
                Create(new ContactData(contactFirstName, contactLastName));
            }
        }

        public ContactHelper Modify(ContactData newData, int index)
        {
            InitContactModification(index);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }

        public ContactHelper Modify(ContactData newData)
        {
            InitContactModification(newData.Id);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }

        public ContactHelper Remove(int index)
        {
            SelectContact(index);
            RemoveContact();
            WaitForContactRemoved();

            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            SelectContact(contact.Id);
            RemoveContact();
            WaitForContactRemoved();

            return this;
        }

        public ContactHelper RemoveAll()
        {
            SelectAllContacts();
            RemoveContact();
            WaitForContactRemoved();

            return this;
        }

        public bool DoesAnyContactExist()
        {
            return driver.FindElement(By.Id("search_count")).Text != "0";
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactsCache = null;
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper WaitForContactRemoved()
        {
            do
            {
                System.Threading.Thread.Sleep(1000);
            } while (driver.FindElement(By.CssSelector("div.msgbox")) == null);
            return this;
        }

        public ContactHelper SelectContact(int contactIndex)
        {
            driver.FindElement(By.XPath($"//*[@id='maintable']/tbody/tr/td[{contactIndex + 1}]/input")).Click();
            return this;
        }

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath($"(//input[@name='selected[]' and @value='{id}'])")).Click();
            return this;
        }

        public ContactHelper SelectAllContacts()
        {
            driver.FindElement(By.Id("MassCB")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactsCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id=" + id + "']")).Click();
            return this;
        }

        public ContactHelper ShowContactDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactsCache = null;
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            // Fill fields from contact class
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("email"), contact.Email);

            return this;
        }

        public ContactHelper GoToContactCreationPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillAdditonalFields()
        {
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys("middleName");
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys("NickName");
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys("company");
            driver.FindElement(By.Name("title")).Clear();
            driver.FindElement(By.Name("title")).SendKeys("title");
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys("address");
            driver.FindElement(By.Name("home")).Clear();
            driver.FindElement(By.Name("home")).SendKeys("123-456-789");
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys("1234567890");
            driver.FindElement(By.Name("work")).Clear();
            driver.FindElement(By.Name("work")).SendKeys("0987654321");
            driver.FindElement(By.Name("fax")).Clear();
            driver.FindElement(By.Name("fax")).SendKeys("1111111111");
            driver.FindElement(By.Name("email2")).Clear();
            driver.FindElement(By.Name("email2")).SendKeys("asd@qwe@com");
            driver.FindElement(By.Name("email3")).Clear();
            driver.FindElement(By.Name("email3")).SendKeys("edc@cde.gov");
            driver.FindElement(By.Name("homepage")).Clear();
            driver.FindElement(By.Name("homepage")).SendKeys("www.nowhere.tk");
            //driver.FindElement(By.Name("bday")).Click();
            //new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText("10");
            //driver.FindElement(By.XPath("//option[@value='10']")).Click();
            //driver.FindElement(By.Name("bmonth")).Click();
            //new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText("October");
            //driver.FindElement(By.XPath("//option[@value='October']")).Click();
            //driver.FindElement(By.Name("byear")).Click();
            //driver.FindElement(By.Name("byear")).Clear();
            //driver.FindElement(By.Name("byear")).SendKeys("1987");
            //driver.FindElement(By.Name("aday")).Click();
            //new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText("12");
            //driver.FindElement(By.XPath("//div[@id='content']/form/select[3]/option[14]")).Click();
            //driver.FindElement(By.Name("amonth")).Click();
            //new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText("August");
            //driver.FindElement(By.XPath("//div[@id='content']/form/select[4]/option[9]")).Click();
            //driver.FindElement(By.Name("ayear")).Clear();
            //driver.FindElement(By.Name("ayear")).SendKeys("2009");

            return this;
        }

        internal ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;

            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        internal ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            return new ContactData(firstName, lastName, email)
            {
                MiddleName = middleName,
                NickName = nickName,
                Company = company,
                Title = title,
                Address = address,
                MobilePhone = mobilePhone,
                HomePhone = homePhone,
                WorkPhone = workPhone,
                Fax = fax,
                Email2 = email2,
                Email3 = email3,
                HomePage = homepage
            };
        }

        public ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            ShowContactDetails(index);

            string rows = driver.FindElement(By.Id("content")).Text;

            string result = Regex
                .Replace(rows, "\n[A-Za-z]+:", "")
                .Replace("\r ", "\n")
                .Replace("\r", "")
                .Replace("\n\n", "\n")
                .Replace(" ", "\n");

            if (result != "")
            {
                result += "\n";
            }

            return new ContactData()
            {
                Details = result
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;

            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
