using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NameAttribute : Attribute
    {
        public NameAttribute(string name)
        {
            Name = name;
        }
        public NameAttribute()
        {

        }
        public string Name { get; set; }
    }
}
