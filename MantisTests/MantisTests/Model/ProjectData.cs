using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisTests
{
    public class ProjectData : IComparable<ProjectData>, IEquatable<ProjectData>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        
        public int CompareTo(ProjectData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return this.ToString().CompareTo(other.ToString());
        }

        public bool Equals(ProjectData other)
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

        public override string ToString()
        {
            return this.Name + this.Description;
        }
    }
}
