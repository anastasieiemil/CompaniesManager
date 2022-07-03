using CompanyManagement.Data.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Repositories.Abstraction
{
    /// <summary>
    /// Exposes data repositories.
    /// Abstract Data Access Layer.
    /// </summary>
    public interface IDALAccessor
    {
        /// <summary>
        /// Companies repository
        /// </summary>
        ICompanyRepository Companies { get; }
    }
}
