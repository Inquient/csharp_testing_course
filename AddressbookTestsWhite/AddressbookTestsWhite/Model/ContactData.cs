using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookTestsWhite
{
    public class ContactData : IComparable<ContactData>, IEquatable<ContactData>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ContactData() { }

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public int CompareTo(ContactData other)
        {
            return this.ToString().CompareTo(other.ToString());
        }

        public bool Equals(ContactData other)
        {
            return this.ToString().Equals(other.ToString());
        }

        public override string ToString()
        {
            return this.FirstName + this.LastName;
        }
    }
}
