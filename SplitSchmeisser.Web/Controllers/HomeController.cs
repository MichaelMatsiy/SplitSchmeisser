﻿using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IUserService userService;
         
        public HomeController(IGroupService groupService, 
            IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            if (CurrentUser.UserName == null)
            {
                CurrentUser.UserName = User.Identity.Name;
            }

            var currentUserId = this.userService.GetCurrUser().Id;

            var gr = await this.groupService.GetGroupsAsync();
                var groups = gr.Select(x => GroupListModel.FromDTO(x)).Where(x => x.UserIDs.Contains(currentUserId))
                .ToList();

            return View(groups);
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
