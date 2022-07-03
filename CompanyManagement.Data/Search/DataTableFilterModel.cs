using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Search
{
    public class DataTableFilterModel
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public List<DataFilter> Columns { get; set; }

        public DataSearch? Search { get; set; }

        public List<DataOrderFilter> Order { get; set; }

        public class DataSearch
        {
            public string? Value { get; set; }
            public bool Regex { get; set; }
        }

        public class DataFilter
        {
            public string? Data { get; set; }
            public string? Name { get; set; }
            public bool Searchable { get; set; }
            public bool Orderable { get; set; }
            public DataSearch? Search { get; set; }
        }

        public class DataOrderFilter
        {
            public int Column { get; set; }
            public string Dir { get; set; }

            public bool IsAsc { 
                get
                {
                    if(Dir?.ToLower() == "asc")
                    {
                        return true;
                    }

                    return false;
                } 
            }

        }

    }
}
