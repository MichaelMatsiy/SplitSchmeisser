using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    //[Authorize]
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

        public IActionResult Index()
        {
            var gr = groupService.GetGroups().Select(x => new GroupViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return View("MyGroups", gr);
        }

        public async Task<IActionResult> Details(int id)
        {
            var op = this.groupService.GetAllOperationsByGroup(id).Result
                .Select(x => OperationViewModel.FromDTO(x));

            //var operations = this.operationService.GetAllOperationsByGroup(id);


            return View("DetailsWithOperations", op);


            //new OperationViewModel
            //{
            //    Id = x.Id,
            //    Amount = x.Amount,
            //    DateOfLoan = x.DateOfLoan,
            //    Description = x.Description,
            //    GroupId = x.Group.Id,
            //    GroupName = x.Group.Name,
            //    OwnerName = x.Owner.Name,
            //    Group = GroupViewModel.FromDTO(x.Group)
            //});

            //var group = await groupService.GetGroupById(id);


            //return View("Details", GroupViewModel.FromDTO(group));
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
            await this.groupService.CreateGroup(group.Name, group.UserIds, group.Amount);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var gr = await groupService.GetGroupById(id);

            var model = new GroupViewModel
            {
                Name = gr.Name
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
                groupService.UpdateGroup(new GroupDTO { Id = group.Id, Name = group.Name });
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.groupService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}