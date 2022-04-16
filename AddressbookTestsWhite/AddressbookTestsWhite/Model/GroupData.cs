using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookTestsWhite
{
    public class GroupData : IComparable<GroupData>, IEquatable<GroupData>
    {
        public string Name { get; set; }

        public GroupData(){}

        public GroupData(string name)
        {
            Name = name;
        }

        public int CompareTo(GroupData other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(GroupData other)
        {
            return this.Name.Equals(other.Name);
        }
    }
}
