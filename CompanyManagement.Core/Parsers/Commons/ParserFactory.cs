using CompanyManagement.Core.Parsers.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core.Parsers.Commons
{
    public class ParserFactory
    {
        /// <summary>
        /// Builds the apropiate Parser.
        /// </summary>
        /// <param name="parserType"></param>
        /// <returns></returns>
        public IParser BuildParser(FileType parserType)
        {
            switch (parserType)
            {
                case FileType.CSV:
                    return new CommaSeparatedValuesParser();
                case FileType.HYPEN_SEPARATED_VALUES:
                    return new HypenSeparatedValuesParser();
                case FileType.HASH_SEPARATED_VALUES:
                    return new HashSeparatedValuesParser();
                default:
                    return null;
            }
        }
    }
}
