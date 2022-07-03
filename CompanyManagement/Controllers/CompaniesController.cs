using CompanyManagement.App_Code.Extensions;
using CompanyManagement.Core.Parsers;
using CompanyManagement.Core.Parsers.Commons;
using CompanyManagement.Data.Models;
using CompanyManagement.Data.Repositories.Abstraction;
using CompanyManagement.Data.Search;
using CompanyManagement.Models.Commons;
using CompanyManagement.Models.Companies;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    public class CompaniesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new CompaniesViewModel();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadFile(FileModel model, [FromServices] ParserFactory factory, [FromServices] IDALAccessor dal)
        {
            if (ModelState.IsValid)
            {
                string message = string.Empty;
                // Build parser.
                var parser = factory.BuildParser(model.FileType);

                using (var stream = model.File.OpenReadStream())
                {
                    // Parse data.
                    var companies = await parser.ParseAsync(stream);

                    // Build db models.
                    var dbCompanies = companies.Select(x => new DbCompany(x)).ToList();

                    // Remove the existing companies from the list.
                    var existingCompanies = await dal.Companies.ExistsAsync(dbCompanies.Select(x => x.CompanyName).ToList());
                    if (existingCompanies.Count > 0)
                    {
                        message = $"The companies: {string.Join(",", existingCompanies.Select(x => x.CompanyName).ToList())} already exist.";
                        dbCompanies = dbCompanies.Where(x => !existingCompanies.Any(y => x.CompanyName == y.CompanyName))
                                                 .ToList();
                    }

                    // Add data to database.
                    var addedCompanies = await dal.Companies.AddRangeAsync(dbCompanies);

                    if (addedCompanies is null || addedCompanies.Count == 0)
                    {
                        return this.BuildStandardRespone($"There was a problem at saving the data. {message}", StandardResponse.ResponseStatus.ERROR);
                    }
                }

                return this.BuildStandardRespone($"The data has succesfully been added.{message}");
            }

            return this.BuildStandardRespone("The sended data is invalid", StandardResponse.ResponseStatus.ERROR);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> Values(DataTableFilterModel dataTableFilters, [FromServices] IDALAccessor dal)
        {
            if (ModelState.IsValid)
            {
                var dbCompanies = await dal.Companies.GetPaginatedCompaniesAsync(dataTableFilters);
                var count = dal.Companies.Count();
                var model = new CompaniesSearchViewModel
                {
                    Data = dbCompanies.Select(x => new CompaniesSearchViewModel.RowData(x)).ToList(),
                    RecordsTotal = count,
                    Draw = dataTableFilters.Draw,
                    RecordsFiltered = count
                };

                return Json(model);
            }

            return Json(null);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> GetCSV([FromBody] DataTableFilterModel dataTableFilters, [FromServices] IDALAccessor dal, [FromServices] ParserFactory factory)
        {
            if (ModelState.IsValid)
            {
                var count = dal.Companies.Count();
                dataTableFilters.Start = 0;
                dataTableFilters.Length = count;

                // Get all companies.
                var dbCompanies = await dal.Companies.GetPaginatedCompaniesAsync(dataTableFilters);
                var companies = dbCompanies.Select(x => x.ToCoreModel()).ToList();

                // Build parser.
                var parser = factory.BuildParser(FileType.CSV);

                if (parser is null)
                {
                    return NotFound();
                }

                var stream = parser.Parse(companies);
                stream.Position = 0;
                return File(stream, "text/csv", "Companies.csv");
            }

            return NotFound();
        }
    }
}
