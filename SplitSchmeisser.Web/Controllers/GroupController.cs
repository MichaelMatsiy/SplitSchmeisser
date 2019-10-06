using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IUserService userService;

        public GroupController(IGroupService groupService,
            IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var groupDto = await this.groupService.GetGroupByIdAsync(id);

            if (groupDto == null) return RedirectToAction("Index", "Home");

            var group = GroupViewModel.FromDTO(groupDto);

            return View("Details", group);
        }

        public async Task<IActionResult> Create()
        {
            var users = await this.userService.GetUsersAsync();

            var model = new GroupCreateModel
            {
                Users = users
                    .Where(x => x.Name != userService.GetCurrUser().Name)
                    .Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        })
                    .ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateModel group)
        {
            await this.groupService.CreateGroupAsync(group.Name, group.UserIds, group.Amount, group.Description);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var group = await groupService.GetGroupByIdAsync(id);

            return View("Edit", GroupEditModel.FromDTO(group));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GroupEditModel group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await groupService.UpdateGroupAsync(new GroupDTO { 
                    Id = group.Id, 
                    Name = group.Name 
                });

                return RedirectToAction("Details", "Group", new { id });
            }
            return View(group);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.groupService.DeleteAsync(id);
            return RedirectToRoute("Default", new
            {
                controller = "",
                action = ""
            });
        }

        public IActionResult RedirectToReports(int? id)
        {   
            return id == null 
                ? RedirectToAction("Groups", "Report") 
                : RedirectToAction("Group", "Report", new { id});
        }

        public IActionResult GenerateReportDebts(int id)
        {
            return RedirectToAction("Debts", "Report", new { id });
        }
    }
}