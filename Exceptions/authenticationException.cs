using Microsoft.AspNetCore.Mvc.Filters;

namespace LMSAPI.exceptions;

public interface IExceptionFilter
{
    void OnException(ExceptionContext filterContext);
}
public class AuthExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext filterContext)
    {
        if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
        {
            filterContext.ExceptionHandled = true;
        }
    }
}