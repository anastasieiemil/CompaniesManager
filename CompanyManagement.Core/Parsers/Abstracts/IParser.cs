using CompanyManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core.Parsers.Abstracts
{

    public interface IParser
    {
        /// <summary>
        /// Tries to parse the given content and tries to build Company object.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<List<Company>> ParseAsync(Stream content);

        /// <summary>
        /// Parse any list of object into specific format .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Stream Parse<T>(List<T> data);
    }
}
