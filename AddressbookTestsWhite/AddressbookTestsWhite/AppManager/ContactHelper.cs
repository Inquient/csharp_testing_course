using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.TableItems;
using TestStack.White.WindowsAPI;

namespace AddressbookTestsWhite
{
    public class ContactHelper : HelperBase
    {
        public static string ContactEditorWinTitle = "Contact Editor";

        public ContactHelper(ApplicationManager manager) : base(manager)
        {

        }

        public List<ContactData> GetContactsList()
        {
            List<ContactData> contacts = new List<ContactData>();
            var dataGrid = manager.MainWindow.Get<Table>("uxAddressGrid");

            foreach (var row in dataGrid.Rows)
            {
                string firstName = row.Cells[0].Value.ToString();
                string lastName = row.Cells[1].Value.ToString();
                contacts.Add(new ContactData(firstName, lastName));
            }

            return contacts;
        }

        public int GetContactsCount()
        {
            var dataGrid = manager.MainWindow.Get<Table>("uxAddressGrid");
            return dataGrid.Rows.Count;
        }

        public void CreateContactIfDoesNotExists()
        {
            if (GetContactsCount() == 0)
            {
                CreateContact(new ContactData("contactToRemoveFirst", "contactToRemoveLastName"));
            }
        }

        public void SelectContactByName(ContactData contact)
        {
            var dataGrid = manager.MainWindow.Get<Table>("uxAddressGrid");
            foreach (var row in dataGrid.Rows)
            {
                if (row.Cells[0].Value.ToString() == contact.FirstName && row.Cells[1].Value.ToString() == contact.LastName)
                {
                    row.Click();
                }
            }
        }

        public void RemoveContact(ContactData contactToRemove)
        {
            SelectContactByName(contactToRemove);

            manager.MainWindow.Get<Button>("uxDeleteAddressButton").Click();
            Window deleteDialog = manager.MainWindow.MessageBox("Question");
            Button okButton = (Button)deleteDialog.Get(SearchCriteria.ByText("Yes"));
            okButton.Click();
        }

        public void CreateContact(ContactData contact)
        {
            Window dialog = OpenContactCreationDialog();

            TextBox firstNameTextBox = dialog.Get<TextBox>("ueFirstNameAddressTextBox");
            TextBox lastNameTextBox = (TextBox)dialog.Get(SearchCriteria.ByAutomationId("ueLastNameAddressTextBox"));

            firstNameTextBox.Text = contact.FirstName;
            lastNameTextBox.Text = contact.LastName;

            dialog.Get<Button>("uxSaveAddressButton").Click();
        }

        private Window OpenContactCreationDialog()
        {
            manager.MainWindow.Get<Button>("uxNewAddressButton").Click();
            return manager.MainWindow.ModalWindow(ContactEditorWinTitle);
        }
    }
}
