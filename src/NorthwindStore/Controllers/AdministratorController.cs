using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindStore.Data.Models;
using NorthwindStore.ViewModels;
using System.Linq;

namespace NorthwindStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly NorthwindContext dboContext;

        public AdministratorController(NorthwindContext dboContext)
        {
            this.dboContext = dboContext;
        }

        // GET
        public IActionResult Index()
        {
            return View(dboContext.Users
                .Select(x => new AdministratorViewModel
                {
                    UserId = x.Id,
                    UserName = x.UserName,
                    UserEmail = x.Email
                }));
        }
    }
}
