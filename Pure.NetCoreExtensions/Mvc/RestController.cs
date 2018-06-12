using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pure.NetCoreExtensions
{
    public abstract class RestController : Controller
    {
        [NonAction]
        public ActionResult Conflict(string type, string title = null)
        {
            return GeneralResponse(StatusCodes.Status409Conflict, type, title);
        }

        [NonAction]
        public ActionResult Gone(string type, string title = null)
        {
            return GeneralResponse(StatusCodes.Status410Gone, type, title);
        }

        [NonAction]
        public ActionResult NotFound(string type, string title = null)
        {
            return GeneralResponse(StatusCodes.Status404NotFound, type, title);
        }

        [NonAction]
        public ActionResult Unauthorized(string type, string title = null)
        {
            return GeneralResponse(StatusCodes.Status401Unauthorized, type, title);
        }

        [NonAction]
        public ActionResult Forbid(string type, string title = null)
        {
            return GeneralResponse(StatusCodes.Status403Forbidden, type, title);
        }

        private ActionResult GeneralResponse(int statusCode, string type, string title)
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Instance = HttpContext.Request.Path
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = statusCode
            };
        }
    }

    public class ProblemDetails
    {
        public int Status {get;set;}
        public string Title { get;set; }
        public string Type { get;set; }
        public PathString Instance { get;set; }
    }
}
