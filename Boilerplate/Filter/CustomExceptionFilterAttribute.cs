using DocumentArchiver.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace DocumentArchiver.Filter
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logger = LogManager.GetLogger(context.ActionDescriptor.DisplayName);
            Utility.LogException(context.Exception, logger);
        }
    }
}
