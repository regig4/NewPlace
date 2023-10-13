using System;
using System.Net;
using Common.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ErrorResponse Error()
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception exception = context.Error;
            HttpStatusCode code = GetStatusCode(exception);

            Response.StatusCode = (int)code;

            return new ErrorResponse(exception);
        }

        public HttpStatusCode GetStatusCode(Exception e)
        {
            return e switch
            {
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
