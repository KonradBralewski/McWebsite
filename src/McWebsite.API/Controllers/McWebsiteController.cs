using ErrorOr;
using McWebsite.API.Common.Http;
using Microsoft.AspNetCore.Mvc;
using McWebsite.Domain.Common.Errors;

namespace McWebsite.API.Controllers
{
    [ApiController]
    public abstract class McWebsiteController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {

            Error firstError = errors.First();
            HttpContext.Items[HttpContextItemKeys.Errors] = errors;

            /// CUSTOM
            if (firstError == Errors.Authentication.InvalidCredentials) {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: firstError.Description);}

            /// DEFAULT

            int httpStatusCode = firstError.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: httpStatusCode, title: firstError.Description);
        }
    }
}
