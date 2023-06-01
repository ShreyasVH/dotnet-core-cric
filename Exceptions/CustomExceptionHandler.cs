using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Com.Dotnet.Cric.Responses;

namespace Com.Dotnet.Cric.Exceptions
{
    public class CustomExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MyException exception)
            {
                var response = new Response(exception.Description);
                context.Result = new ObjectResult(response)
                {
                    StatusCode = exception.HttpStatusCode
                };
                context.ExceptionHandled = true;
            }
        }
    }
}