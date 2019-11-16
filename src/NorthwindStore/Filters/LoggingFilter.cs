using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NorthwindStore.Extensions;

namespace NorthwindStore.Filters
{
    public class LoggingFilter : IResultFilter
    {
        private readonly ILogger<LoggingFilter> log;
        private readonly LoggingFilterConfiguration configuration;

        public LoggingFilter(ILogger<LoggingFilter> log, LoggingFilterConfiguration configuration)
        {
            this.log = log;
            this.configuration = configuration;
        }

        public LoggingFilter()
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller == null)
                return;

            if (configuration.LogParameters)
            {
                string message = new
                {
                    ActionPosition = "Start",
                    ControllerName = controller.ControllerContext.ActionDescriptor.ControllerName,
                    ActionName = controller.ControllerContext.ActionDescriptor.ActionName,
                    Parameters = string.Join(';', context.GetActionParameters().Select(x => $"[{x.Key}]:{x.Value}"))
                }.ToString();
                log.LogInformation(message);
            }
            else
            {
                string message = new
                {
                    ActionPosition = "Start",
                    ControllerName = controller.ControllerContext.ActionDescriptor.ControllerName,
                    ActionName = controller.ControllerContext.ActionDescriptor.ActionName
                }.ToString();
                log.LogInformation(message);
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            var controller = context.Controller as Controller;
            if (controller == null)
                return;

            if (configuration.LogParameters)
            {
                var message = new
                {
                    ActionPosition = "End",
                    ControllerName = controller.ControllerContext.ActionDescriptor.ControllerName,
                    ActionName = controller.ControllerContext.ActionDescriptor.ActionName,
                    Parameters = string.Join(';', context.GetActionParameters().Select(x => $"[{x.Key}]:{x.Value}"))
                }.ToString();
                log.LogInformation(message);
            }
            else
            {
                var message = new
                {
                    ActionPosition = "End",
                    ControllerName = controller.ControllerContext.ActionDescriptor.ControllerName,
                    ActionName = controller.ControllerContext.ActionDescriptor.ActionName
                }.ToString();
                log.LogInformation(message);
            }
        }
    }
}
