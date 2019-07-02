using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    public class GroupController : Controller
    {
        IGroupServise groupService;

        public GroupController(IGroupServise groupService)
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

            return View("MyGroups", gr);
        }

        public IActionResult Details(int id)
        {
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
            throw new NotImplementedException();
        }

        public IActionResult Edit(int id)
        {
            var gr = groupService.GetGroups()
                .Where(x => x.Id == id)
                .Select(x => new GroupViewModel
                {
                    Id = x.Id,
                    GroupName = x.Name
                })
            .FirstOrDefault();

            return View("Edit", gr);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName")] GroupViewModel group)
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