using CompanyManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Models
{
    public class DbCompany
    {
        public DbCompany()
        {

        }

        public DbCompany(Company company)
        {
            CompanyName = company.CompanyName;
            YearFounded = company.YearFounded;
            ContactName = company.ContactName;
            ContactEmail = company.ContactEmail;
            ContactPhone = company.ContactPhone;
        }

        public Company ToCoreModel()
        {
            return new Company
            {
                CompanyName = CompanyName,
                YearFounded = YearFounded,
                ContactName = ContactName,
                ContactEmail = ContactEmail ?? string.Empty,
                ContactPhone = ContactPhone,

            };
        }
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public int YearFounded { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
    }
}
