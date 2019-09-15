using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SplitSchmeisser.BLL;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IGroupService groupService;
        IReportService reportService;
        IUserService userService;
        IOperationService operationService;
         

        public HomeController(IGroupService groupService, 
            IReportService reportService,
            IOperationService operationService,
            IUserService userService)
        {
            this.groupService = groupService;
            this.reportService = reportService;
            this.operationService = operationService;
            this.userService = userService;
        }

        public async Task<IActionResult> Updator() {

            var rep = await this.reportService.GetReport(new BLL.Models.ReportRequest
            {
                StartDate = DateTime.Now.AddYears(-1),
                EndDate = DateTime.Now.AddYears(10),
                GroupId = 1
            });

            var result = rep.Select(x => OperationDTO.FromEntity(x))
                .Select(x=> OperationViewModel.FromDTO(x)).ToList();

            return View("Report", result);
        }

        public IActionResult Index()
        {
            if (CurrentUserService.UserName == null)
            {
                CurrentUserService.UserName = User.Identity.Name;
            }

            var currentUserId = this.userService.GetCurrUser().Id;

            var gr = this.groupService.GetGroups()
                .Select(x => GroupListModel.FromDTO(x)).Where(x => x.UserIDs.Contains(currentUserId))
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
