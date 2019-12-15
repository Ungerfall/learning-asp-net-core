using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindStore.ComponentModel.Design;
using NorthwindStore.ViewModels;

namespace NorthwindStore.ViewComponents
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        private readonly ILinkedBreadcrumbsFactory breadcrumbsFactory;

        public BreadcrumbsViewComponent(ILinkedBreadcrumbsFactory breadcrumbsFactory)
        {
            this.breadcrumbsFactory = breadcrumbsFactory;
        }

        public IViewComponentResult Invoke(HttpContext context)
        {
            if (
                !context.Request.RouteValues.TryGetValue("controller", out var controllerName)
                || !context.Request.RouteValues.TryGetValue("action", out var controllerAction)
            )
            {
                return View((BreadcrumbsViewModel)null);
            }

            var crumbs = breadcrumbsFactory.Create(controllerName.ToString(), controllerAction.ToString());
            return View(new BreadcrumbsViewModel {Breadcrumbs = crumbs});
        }
    }
}
