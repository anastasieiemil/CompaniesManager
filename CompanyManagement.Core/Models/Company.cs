using CompanyManagement.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core.Models
{
    public class Company
    {
        [ParserColumnOrder(0)]
        public string CompanyName { get; set; }

        [ParserIgnore]
        public int YearFounded { get; set; }
        [ParserColumnOrder(1)]
        public int YearsInBusiness {
            get
            {
                return DateTime.Now.Year - YearFounded;    
            }
        }

        [ParserColumnOrder(2)]
        public string ContactName { get; set; }

        [ParserColumnOrder(3)]
        public string ContactPhone { get; set; }

        [ParserColumnOrder(4)]
        public string ContactEmail { get; set; }
    }
}
