using CompanyManagement.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core.Parsers.Commons
{
    public enum FileType
    {
        [Name("Comma Separated Values")]
        CSV = 0,
        [Name("Hyphen Separated Values")]
        HYPEN_SEPARATED_VALUES = 1,
        [Name("Hash Separated Values")]
        HASH_SEPARATED_VALUES = 2,

    }
}
