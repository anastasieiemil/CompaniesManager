using CompanyManagement.Core;
using CompanyManagement.Core.Attributes;
using CompanyManagement.Core.Parsers.Commons;

namespace CompanyManagement.Models.Companies
{
    public class CompaniesViewModel
    {
        public CompaniesViewModel()
        {
            FileTypes = Enum.GetValues<FileType>().Select(x => new Option
            {
                Name = Utils.GetAttributeOfType<NameAttribute>(x)?.Name ?? string.Empty,
                Value = (int)x,
            }).ToList();
        }
        public List<Option> FileTypes { get; set; }
        public class Option
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
    }
}
