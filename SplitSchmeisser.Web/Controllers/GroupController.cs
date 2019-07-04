using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    public class GroupController : Controller
    {
        IGroupServise groupService;
        IUserServise userService;


        public GroupController(IGroupServise groupService, IUserServise userService)
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
            var gr1 = await groupService.GetGroupById(id);
            var gr = groupService.GetGroups()
                .Where(x => x.Id == id)
                .Select(x => new GroupViewModel
                {
                    Id = x.Id,
                    GroupName = x.Name
                })
            .FirstOrDefault();

            return View("Details", gr);
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