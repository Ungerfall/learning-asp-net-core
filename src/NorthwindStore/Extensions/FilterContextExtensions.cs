using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindStore.Extensions
{
    public static class FilterContextExtensions
    {
        public static IReadOnlyDictionary<string, string> GetActionParameters(this ResultExecutedContext context)
        {
            var ctrl = context.Controller as Controller;
            return ctrl == null
                ? new Dictionary<string, string>()
                : GetActionParameters(ctrl);
        }

        public static IReadOnlyDictionary<string, string> GetActionParameters(this ResultExecutingContext context)
        {
            var ctrl = context.Controller as Controller;
            return ctrl == null
                ? new Dictionary<string, string>()
                : GetActionParameters(ctrl);
        }

        private static IReadOnlyDictionary<string, string> GetActionParameters(Controller ctrl)
        {
            return ctrl
                .ControllerContext
                .ActionDescriptor
                .Parameters
                .ToDictionary(x => x.Name, x => ctrl.RouteData.Values[x.Name]?.ToString());
        }
    }
}
