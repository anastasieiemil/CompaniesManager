using CompanyManagement.Data.Repositories.Abstraction;
using CompanyManagement.Data.Repositories.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Repositories
{
    public class DALAccessor : IDALAccessor
    {
        public DALAccessor(CompanyManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ICompanyRepository Companies
        {
            get
            {
                if (companies is null)
                {
                    companies = new CompanyRepository(dbContext);
                }

                return companies;
            }
        }


        private ICompanyRepository? companies = null;
        private readonly CompanyManagementDbContext dbContext;
    }
}
