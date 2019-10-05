using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Xml.Serialization;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.Web.Models;
using System.IO;
using System.Collections.Generic;
using SplitSchmeisser.BLL.CommonLogic;

namespace SplitSchmeisser.Web.Controllers
{
    public class OperationController : Controller
    {
        IGroupService groupService;
        IUserService userService;
        IOperationService operationService;

        public OperationController(IGroupService groupService,
            IUserService userService,
            IOperationService operationService)
        {
            this.groupService = groupService;
            this.userService = userService;
            this.operationService = operationService;
        }

        public IActionResult Create(int id)
        {
            return View(new OperationCreateModel
            {
                GroupID = id,
                OwnerID = this.userService.GetCurrUser().Id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperationCreateModel op)
        {
            await this.operationService.CreateOperation(
                op.OwnerID,
                op.GroupID,
                op.Amount,
                op.Description
             );

            return RedirectToAction("Details",
                new RouteValueDictionary(
                    new
                    {
                        controller = "Group",
                        action = "Details",
                        Id = op.GroupID
                    }));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var operationDto = await operationService.GetOperationById(id);

            return View("Edit", OperationEditModel.FromDTO(operationDto));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Amount,GroupId")] OperationEditModel operation)
        {
            if (id != operation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await operationService.UpdateOperation(new OperationDTO {
                    Id = operation.Id,
                    Description = operation.Description,
                    Amount = operation.Amount
                });

                return RedirectToAction("Details",
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Group",
                            action = "Details",
                            Id = operation.GroupId
                        }));
            }

            return View(operation);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var groupId = this.operationService.GetOperations()
                .First(x => x.Id == id)
                .GroupId;

            await this.operationService.DeleteAsync(id);

            var opCount = this.operationService
                .GetAllOperationsByGroup(groupId)
                .Count;

            if (opCount > 0)
            {
                return RedirectToAction("Details",
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Group",
                            action = "Details",
                            Id = groupId
                        }));
            }
            else {

                await this.groupService.Delete(groupId);
                return RedirectToRoute("Default", new {
                    controller = "",
                    action = ""
                });
            }
        }

        public IActionResult RedirectToReports(OperationReportTypes type, int id)
        {
            return type == OperationReportTypes.Group
                ? RedirectToAction("Operations", "Report", new { groupId = id })
                : RedirectToAction("Operation", "Report", new { operationId = id });
        }
    }
}