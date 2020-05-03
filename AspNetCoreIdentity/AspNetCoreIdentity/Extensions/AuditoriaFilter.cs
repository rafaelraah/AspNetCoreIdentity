using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Extensions
{
    public class AuditoriaFilter : IActionFilter
    {
        private readonly KissLog.ILogger _logger;

        public AuditoriaFilter(KissLog.ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + " acessou " +
                              context.HttpContext.Request.GetDisplayUrl();

                _logger.Info(message);

            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
