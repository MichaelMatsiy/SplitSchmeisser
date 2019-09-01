using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IGroupService groupService;

        public HomeController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public IActionResult Index()
        {
            var gr = this.groupService.GetGroups()
                .Select(x => new GroupViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
            .ToList();

            return View(gr);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
