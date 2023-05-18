using ErrorOr;
using McWebsite.API.Common.Http;
using Microsoft.AspNetCore.Mvc;
using McWebsite.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace McWebsite.API.Controllers
{
    [ApiController]
    public abstract class McWebsiteController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if(errors.Count is 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            Error firstError = errors.First();
            HttpContext.Items[HttpContextItemKeys.Errors] = errors;

            /// CUSTOM
            if (firstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: firstError.Description);
            }

            /// DEFAULT

            return Problem(firstError);
        }

        private IActionResult Problem(Error error)
        {
            int httpStatusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return base.Problem(statusCode: httpStatusCode, title: error.Description);
        }

        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }

            return base.ValidationProblem(modelStateDictionary);
        }
    }
}
