using CompanyManagement.Data.Models;
using CompanyManagement.Models.Commons.Search;

namespace CompanyManagement.Models.Companies
{
    public class CompaniesSearchViewModel : DataTableModel<CompaniesSearchViewModel.RowData>
    {
        public class RowData
        {
            public RowData(DbCompany dbCompany)
            {
                ID = dbCompany.ID;
                CompanyName = dbCompany.CompanyName;
                YearInBusiness = DateTime.Now.Year - dbCompany.YearFounded;
                ContactName = dbCompany.ContactName;
                ContactPhone = dbCompany.ContactPhone;
                ContactEmail = dbCompany.ContactEmail;
            }
            public int ID { get; set; }
            public string CompanyName { get; set; }
            public int YearInBusiness { get; set; }
            public string ContactName { get; set; }
            public string ContactPhone { get; set; }
            public string? ContactEmail { get; set; }
        }
    }
}
