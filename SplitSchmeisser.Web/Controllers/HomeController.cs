using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IGroupServise groupService;

        public HomeController(IGroupServise groupService)
        {
            this.groupService = groupService;
        }

        public IActionResult Index()
        {
            var gr = groupService.GetGroups().Select(x => new GroupViewModel
            {
                Id = x.Id,
                GroupName = x.Name
            }).ToList();

            return View(gr);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
