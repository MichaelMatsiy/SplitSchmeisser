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
        IGroupService groupService;
        IUserService userService;
        IOperationService operationService;

        public GroupController(IGroupService groupService, 
            IUserService userService,
            IOperationService operationService)
        {
            this.groupService = groupService;
            this.userService = userService;
            this.operationService = operationService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var groupDto = await this.groupService.GetGroupById(id);
            groupDto.UserDebts = await this.operationService.GetUsersDebtByGroup(groupDto);
            
            var group = GroupViewModel.FromDTO(groupDto);

            return View("Details", group);
        }

        public IActionResult Create()
        {
            var users = this.userService.GetUsers();
            var model = new GroupCreateModel
            {
                Users = users.Select(x=> new SelectListItem {
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
            await this.groupService.CreateGroup(group.Name, group.UserIds, group.Amount);

            return RedirectToRoute("Default", new
            {
                controller = "",
                action = ""
            });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var group = await groupService.GetGroupById(id);

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
                await groupService.UpdateGroup(new GroupDTO { Id = group.Id, Name = group.Name });

                return RedirectToAction("Details",
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Group",
                            action = "Details",
                            Id = id
                        }));
            }
            return View(group);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.groupService.Delete(id);
            return RedirectToRoute("Default", new
            {
                controller = "",
                action = ""
            });
        }
    }
}