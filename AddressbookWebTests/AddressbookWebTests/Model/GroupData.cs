using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace AddressbookWebTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        public GroupData()
        {
        }

        public GroupData(string name)
        {
            Name = name;
        }

        public GroupData(string name, string header, string footer)
        {
            Name = name;
            Header = header;
            Footer = footer;
        }

        [Column(Name = "group_name")]
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; }

        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<GroupData> GetAll()
        {
            using (AddresBookDB db = new AddresBookDB())
            {
                return (from g in db.groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddresBookDB db = new AddresBookDB())
            {
                return (from c in db.Contacts 
                        from gcr in db.GCR.Where(p => p.GroupId == this.Id && p.ContactId == c.Id)
                        select c).Distinct().ToList();
            }
        }

        //Позволяет проверить, равны ли два объекта
        public bool Equals(GroupData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        //Позволяет ускорить сравнение объектов
        //Cначала проверяется HashCode и если они не совпадают, то идёт проверка через Equals
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + Name
                + "\nheader = " + Header
                + "\nfooter = " + Footer;
        }

        //Позволяет отсортировать коллекцию объектов по определённому правилу
        //Если
        public int CompareTo(GroupData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }
    }
}
