using ApiTemplApiProject.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplApiProject.Web.Entry.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ISysUserService _systemService;

        public HomeController(ISysUserService systemService)
        {
            _systemService = systemService;
        }

        public IActionResult Index()
        {
            ViewBag.Description = _systemService.GetUserById(1);

            return View();
        }
    }
}
