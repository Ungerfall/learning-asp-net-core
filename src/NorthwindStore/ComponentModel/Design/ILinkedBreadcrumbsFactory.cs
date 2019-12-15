using System.Collections.Generic;

namespace NorthwindStore.ComponentModel.Design
{
    public interface ILinkedBreadcrumbsFactory
    {
        LinkedList<Breadcrumb> Create(string topControllerName, string controllerAction);
    }
}
