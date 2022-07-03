using CompanyManagement.App_Code.DataAnnotations;
using CompanyManagement.Core.Parsers.Commons;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.Models.Companies
{
    public class FileModel
    {
        [Required, MaxSize(20971520)] // 20 MB
        public IFormFile File { get; set; }

        [Required, EnumDefined(typeof(FileType))]
        public FileType FileType { get; set; }
    }
}
