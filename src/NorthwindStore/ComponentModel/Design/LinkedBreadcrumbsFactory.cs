using System.Collections.Generic;

namespace NorthwindStore.ComponentModel.Design
{
    public class LinkedBreadcrumbsFactory : ILinkedBreadcrumbsFactory
    {
        private readonly Dictionary<string, string> childParent = new Dictionary<string, string>
        {
            ["Product"] = "Home",
            ["Category"] = "Home",
            ["Administrator"] = "Home"
        };

        private readonly Dictionary<(string, string), string> controllerActionByBreadcrumbText =
            new Dictionary<(string contoller, string action), string>
            {
                [(contoller: "Home", action: "Index")] = "Home",
                [(contoller: "Product", action: "Index")] = "Products",
                [(contoller: "Product", action: "Create")] = "Create New",
                [(contoller: "Product", action: "Edit")] = "Edit",
                [(contoller: "Category", action: "Index")] = "Categories",
                [(contoller: "Category", action: "Create")] = "Create New",
                [(contoller: "Category", action: "Edit")] = "Edit",
                [(contoller: "Administrator", action: "Index")] = "Administrator"
            };

        public LinkedList<Breadcrumb> Create(string topControllerName, string controllerAction)
        {
            // Home is top ancestor, so skip
            if (topControllerName == "Home")
                return null;

            var linkedBreadcrumbs = CreateParentCrumbs(topControllerName);
            if (controllerAction != "Index")
            {
                // add crumb for the controller action
                linkedBreadcrumbs.AddLast(
                    new Breadcrumb
                    {
                        Action = controllerAction,
                        Controller = topControllerName,
                        Text = controllerActionByBreadcrumbText[(topControllerName, controllerAction)]
                    });
            }

            return linkedBreadcrumbs;
        }

        private LinkedList<Breadcrumb> CreateParentCrumbs(string controllerName)
        {
            string child = controllerName;
            bool hasParent = childParent.TryGetValue(child, out var parent);
            if (!hasParent)
            {
                return new LinkedList<Breadcrumb>();
            }

            var parents = new Stack<string>();
            parents.Push(controllerName);
            // push all patents
            while (hasParent)
            {
                parents.Push(parent);
                child = parent;
                hasParent = childParent.TryGetValue(child, out parent);
            }

            var crumbs = new LinkedList<Breadcrumb>();
            parent = parents.Pop();

            // fill linked list with all ancestors
            var childNode = crumbs.AddFirst(new Breadcrumb
            {
                Action = "Index",
                Controller = parent,
                Text = controllerActionByBreadcrumbText[(parent, "Index")]
            });
            while (parents.Count > 0)
            {
                parent = parents.Pop();
                childNode = crumbs.AddAfter(
                    childNode,
                    new Breadcrumb
                    {
                        Action = "Index",
                        Controller = parent,
                        Text = controllerActionByBreadcrumbText[(parent, "Index")]
                    }
                );
            }

            return crumbs;
        }
    }
}
