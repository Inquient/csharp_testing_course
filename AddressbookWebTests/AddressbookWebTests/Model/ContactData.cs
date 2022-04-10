using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace AddressbookWebTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string details;

        public ContactData() { }

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactData(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }
        
        [Column(Name = "middlename")]
        public string MiddleName { get; set; }
        
        [Column(Name = "lastname")]
        public string LastName { get; set; }

        public string NickName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string HomePage { get; set; }
        public string Birthday { get; set; }
        public string Anniversuary { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (Email + "\r\n" + Email2 + "\r\n" + Email3).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string Details
        {
            get
            {
                if (details != null)
                {
                    return details;
                }
                else
                {
                    return Format(FirstName)
                        + Format(MiddleName)
                        + Format(LastName)
                        + Format(NickName)
                        + Format(Title)
                        + Format(Company)
                        + Format(Address)
                        + Format(HomePhone)
                        + Format(MobilePhone)
                        + Format(WorkPhone)
                        + Format(Fax)
                        + Format(Email)
                        + Format(Email2)
                        + Format(Email3)
                        + Format(HomePage);
                }
            }
            set
            {
                details = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ \\-()]", "") + "\r\n";
        }

        private string Format(string s)
        {
            if (s == null || s == "")
            {
                return "";
            }
            return s + "\n";
        }

        public static List<ContactData> GetAll()
        {
            using (AddresBookDB db = new AddresBookDB())
            {
                return (from c in db.Contacts select c).ToList();
            }
        }

        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            return this.ToString() == other.ToString();
        }

        public override int GetHashCode()
        {
            return (this.ToString()).GetHashCode();
        }

        public override string ToString()
        {
            return "FirstName = " + FirstName
                                  + "\nLastName = " + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return this.ToString().CompareTo(other.ToString());
        }


    }
}
