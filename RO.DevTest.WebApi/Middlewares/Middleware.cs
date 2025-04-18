using System.Net;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.WebApi.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (BadRequestException ex)
            {
                httpContext.Response.StatusCode = (int)ex.StatusCode;
                httpContext.Response.ContentType = "application/json";

                var response = ex.GetErrorResponse();
                await httpContext.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro inesperado."
                };

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}