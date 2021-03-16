using Common.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = GetStatusCode(exception);

            Response.StatusCode = (int) code;

            return new ErrorResponse(exception);
        }

        public HttpStatusCode GetStatusCode(Exception e) =>
            e switch
            {
                _ => HttpStatusCode.InternalServerError
            };
    }
}
