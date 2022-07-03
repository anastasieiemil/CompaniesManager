using CompanyManagement.Core.Models;
using CompanyManagement.Core.Parsers.Abstracts;
using CompanyManagement.Core.Parsers.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core.Parsers
{
    public class HashSeparatedValuesParser : IParser
    {
        public Stream Parse<T>(List<T> data)
        {
            return ParserUtils.Parse(data, "#");
        }

        public async Task<List<Company>> ParseAsync(Stream content)
        {
            var lines = await ParserUtils.GetLinesAsync(content);
            if (lines is null)
            {
                return null;
            }


            List<Company> companies = new List<Company>();
            foreach (var line in lines)
            {
                var tokens = ParserUtils.GetTokens(line, "#");

                // Skip lines that have missing data.
                if (tokens.Count != 4)
                {
                    continue;
                }

                // Skip lines that have invalid data.
                if (!int.TryParse(tokens[ColumnsType.YEAR_FOUNDED], out int yearFounded))
                {
                    continue;
                }
                var company = new Company
                {
                    CompanyName = tokens[ColumnsType.COMPANY_NAME],
                    YearFounded = yearFounded,
                    ContactName = tokens[ColumnsType.CONTACT_NAME],
                    ContactPhone = tokens[ColumnsType.CONTACT_PHONE],
                };

                companies.Add(company);
            }

            return companies;
        }

        #region private
        private static class ColumnsType
        {
            public static readonly int COMPANY_NAME = 0;
            public static readonly int YEAR_FOUNDED = 1;
            public static readonly int CONTACT_NAME = 2;
            public static readonly int CONTACT_PHONE = 3;
        }

        #endregion

    }
}
