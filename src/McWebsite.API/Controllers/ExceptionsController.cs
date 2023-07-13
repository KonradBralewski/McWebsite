using McWebsite.API.Controllers.Base;
using McWebsite.Domain.Common.Errors;
using McWebsite.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace McWebsite.API.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("exceptions")]
    public class ExceptionsController : McWebsiteController
    {
        public IActionResult Exceptions()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>()!.Error;

            Log.Error(exception, exception.Message);

            if (exception.Data["error"] is ErrorOr.Error)
            {
                ErrorOr.Error error = (ErrorOr.Error)exception.Data["error"]!;

                return Problem(new List<ErrorOr.Error>() { error });
            }

            return Problem(statusCode: StatusCodes.Status500InternalServerError,
                           title: "System error",
                           detail: "Unexpected exception has occured while processing your request.");
        }
    }
}
