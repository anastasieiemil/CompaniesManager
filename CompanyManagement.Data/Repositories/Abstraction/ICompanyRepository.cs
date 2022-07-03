using CompanyManagement.Data.Models;
using CompanyManagement.Data.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Repositories.Abstraction
{
    public interface ICompanyRepository
    {
        /// <summary>
        /// Add multiple companies.
        /// </summary>
        /// <param name="companies"></param>
        /// <returns></returns>
        List<DbCompany> AddRange(List<DbCompany> companies);

        /// <summary>
        /// Add multiple companies async.
        /// </summary>
        /// <param name="companies"></param>
        /// <returns></returns>
        Task<List<DbCompany>> AddRangeAsync(List<DbCompany> companies);

        /// <summary>
        /// Add company.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        DbCompany Add(DbCompany company);

        /// <summary>
        /// Add company async.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        Task<DbCompany> AddAsync(DbCompany company);

        /// <summary>
        /// Gets all companies using filters.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<DbCompany> GetPaginatedCompanies(DataTableFilterModel filters);

        /// <summary>
        /// Gets all companies using filters async.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<List<DbCompany>> GetPaginatedCompaniesAsync(DataTableFilterModel filters);

        /// <summary>
        /// Returns the total number of records.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Returns a list with all companies that exists.
        /// The query is made from the intersection with names list.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        List<DbCompany> Exists(List<string> names);

        /// <summary>
        /// Returns a list with all companies that exists async.
        /// The query is made from the intersection with names list.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        Task<List<DbCompany>> ExistsAsync(List<string> names);
    }
}
