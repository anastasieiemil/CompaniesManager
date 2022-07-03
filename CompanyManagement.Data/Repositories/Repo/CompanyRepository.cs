using CompanyManagement.Data.Models;
using CompanyManagement.Data.Repositories.Abstraction;
using CompanyManagement.Data.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Repositories.Repo
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyManagementDbContext dbContext;
        public CompanyRepository(CompanyManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DbCompany Add(DbCompany company)
        {
            try
            {
                dbContext.Companies.Add(company);
                dbContext.SaveChanges();
            }
            catch
            {
                return null;
            }

            return company;
        }

        public async Task<DbCompany> AddAsync(DbCompany company)
        {
            try
            {
                dbContext.Companies.Add(company);
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            return company;
        }

        public List<DbCompany> AddRange(List<DbCompany> companies)
        {
            try
            {
                dbContext.Companies.AddRange(companies);
                dbContext.SaveChanges();
            }
            catch
            {
                return null;
            }

            return companies;
        }

        public async Task<List<DbCompany>> AddRangeAsync(List<DbCompany> companies)
        {
            try
            {
                dbContext.Companies.AddRange(companies);
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            return companies;
        }

        public int Count()
        {
            return dbContext.Companies.Count();
        }

        public List<DbCompany> Exists(List<string> names)
        {
            var query = dbContext.Companies.AsNoTracking();
            query = query.Where(x => names.Any(y => y == x.CompanyName));

            return query.ToList();
        }

        public async Task<List<DbCompany>> ExistsAsync(List<string> names)
        {
            var query = dbContext.Companies.AsNoTracking();
            query = query.Where(x => names.Any(y => y == x.CompanyName));

            return await query.ToListAsync();
        }

        public List<DbCompany> GetPaginatedCompanies(DataTableFilterModel filters)
        {
            var query = BuildQuery(filters);
            if (query is null)
                return new List<DbCompany>();

            return query.ToList();
        }

        public async Task<List<DbCompany>> GetPaginatedCompaniesAsync(DataTableFilterModel filters)
        {
            var query = BuildQuery(filters);
            if (query is null)
                return new List<DbCompany>();

            return await query.ToListAsync();
        }

        #region private

        /// <summary>
        /// Builds query using filters for setting order and pagination.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        private IQueryable<DbCompany> BuildQuery(DataTableFilterModel filters)
        {
            var query = dbContext.Companies.AsNoTracking();

            // Order data.
            var order = filters.Order[0];

            if (order is null)
            {
                return null;
            }

            if (order.IsAsc)
            {
                // Check if is years in business.
                if (order.Column == 1)
                {
                    query = query.OrderByDescending(GetSortColumn(order.Column)).ThenBy(x => x.CompanyName);
                }
                else
                {
                    query = query.OrderBy(GetSortColumn(order.Column));
                }
            }
            else
            {
                if (order.Column == 1)
                {
                    query = query.OrderBy(GetSortColumn(order.Column)).ThenByDescending(x => x.CompanyName);
                }
                else
                {
                    query = query.OrderByDescending(GetSortColumn(order.Column));
                }
            }

            // Pagination.
            query = query.Skip(filters.Start).Take(filters.Length);

            return query;
        }

        /// <summary>
        /// Selects column for witch the sort will be performed.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private System.Linq.Expressions.Expression<Func<DbCompany, object>> GetSortColumn(int column)
        {
            switch (column)
            {
                case 0:
                    return (i) => i.CompanyName;
                case 1:
                    return (i) => i.YearFounded;
                case 2:
                    return (i) => i.ContactName;
                case 3:
                    return (i) => i.ContactPhone;

                default:
                    return (i) => i.ID;
            }


        }
        #endregion
    }
}
