using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.Web.Models;
using System.Xml.Serialization;
using System.IO;
using SplitSchmeisser.BLL.CommonLogic;
using System.Collections.Generic;

namespace SplitSchmeisser.Web.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        IGroupService groupService;
        IUserService userService;

        public GroupController(IGroupService groupService,
            IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var groupDto = await this.groupService.GetGroupById(id);
            groupDto.UserDebts = await this.userService.GetUserDebtsByGroupPerUrers(groupDto);

            var group = GroupViewModel.FromDTO(groupDto);

            return View("Details", group);
        }

        public IActionResult Create()
        {
            var users = this.userService.GetUsers();
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

        public async Task<IActionResult> GenerateReportGroup(int id)
        {
            var dto = await this.groupService.GetGroupById(id);
            dto.UserDebts = await this.userService.GetUserDebtsByGroupPerUrers(dto);

            byte[] bytes = null;
            XmlSerializer xs = new XmlSerializer(typeof(GroupViewModel));

            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, GroupViewModel.FromDTO(dto));
                bytes = ms.ToArray();
            }

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = $"{dto.Name}.xml",
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(bytes, System.Net.Mime.MediaTypeNames.Text.Xml);
        }

        public async Task<IActionResult> GenerateReportGroups()
        {
            var currUser = this.userService.GetCurrUser();
            var gr = this.groupService.GetGroups()
                .Select(x => GroupListModel.FromDTO(x))
                .Where(x => x.UserIDs.Contains(currUser.Id))
                .ToList();

            byte[] bytes = null;
            XmlSerializer xs = new XmlSerializer(typeof(List<GroupListModel>));

            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, gr);
                bytes = ms.ToArray();
            }

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = $"{currUser.Name} - Groups.xml",
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(bytes, System.Net.Mime.MediaTypeNames.Text.Xml);
        }

        public async Task<IActionResult> GenerateReportDebts(int id)
        {
            var dto = await this.groupService.GetGroupById(id);
            var userDebts = await this.userService.GetUserDebtsByGroupPerUrers(dto);

            byte[] bytes = null;
            XmlSerializer xs = new XmlSerializer(typeof(List<Debt>));

            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, userDebts);
                bytes = ms.ToArray();
            }

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = $"{dto.Name} - UserDebts.xml",
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(bytes, System.Net.Mime.MediaTypeNames.Text.Xml);
        }
    }
}