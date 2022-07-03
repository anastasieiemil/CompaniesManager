using CompanyManagement.Models.Commons;
using Microsoft.AspNetCore.Mvc;
using static CompanyManagement.Models.Commons.StandardResponse;

namespace CompanyManagement.App_Code.Extensions
{
    public static class ControllerExtension
    {
        public static IActionResult BuildStandardRespone(this Controller controller, string message = "", ResponseStatus status = ResponseStatus.SUCCESS)
        {
            return controller.Json(new StandardResponse 
            { 
                Message = message,
                Status = status
            });
        }
    }
}
