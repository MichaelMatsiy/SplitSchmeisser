using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    //[Authorize]
    public class GroupController : Controller
    {
        IGroupService groupService;
        IUserService userService;

        public GroupController(IGroupService groupService, IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var gr = groupService.GetGroups().Select(x => new GroupViewModel
            {
                Id = x.Id,
                GroupName = x.Name
            }).ToList();

            return View("MyGroups", gr);
        }

        public async Task<IActionResult> Details(int id)
        {
            var group = await groupService.GetGroupById(id);
            return View("Details", GroupViewModel.FromDTO(group));
        }

        public IActionResult Create()
        {
            var users = this.userService.GetUsers();
            var model = new CreateGroupModel {
                Users = users.Select(x=> new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupModel group)
        {
            await this.groupService.CreateGroup(group.GroupName, group.UserIds, group.Amount);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var gr = await groupService.GetGroupById(id);

            var model = new GroupViewModel
            {
                GroupName = gr.Name
            };

            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,GroupName")] GroupViewModel group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                groupService.UpdateGroup(new BLL.Models.GroupDTO { Id = group.Id, Name = group.GroupName });
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}