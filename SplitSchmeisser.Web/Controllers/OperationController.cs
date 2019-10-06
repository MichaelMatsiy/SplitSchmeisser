using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.Web.Models;
using SplitSchmeisser.BLL.CommonLogic;

namespace SplitSchmeisser.Web.Controllers
{
    public class OperationController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IUserService userService;
        private readonly IOperationService operationService;

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
            await this.operationService.CreateOperationAsync(
                op.OwnerID,
                op.GroupID,
                op.Amount,
                op.Description
             );

            return RedirectToAction("Details", "Group", new { Id = op.GroupID });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var operationDto = await operationService.GetOperationByIdAsync(id);

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
                await operationService.UpdateOperationAsync(new OperationDTO {
                    Id = operation.Id,
                    Description = operation.Description,
                    Amount = operation.Amount
                });

                return RedirectToAction("Details", "Group", new { Id = operation.GroupId });
            }

            return View(operation);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var operationDto = await this.operationService.GetOperationByIdAsync(id);

            await this.operationService.DeleteAsync(operationDto);

            return RedirectToAction("Details", "Group", new { id = operationDto.GroupId });
        }

        public IActionResult RedirectToReports(OperationReportTypes type, int id)
        {
            return type == OperationReportTypes.Group
                ? RedirectToAction("Operations", "Report", new { groupId = id })
                : RedirectToAction("Operation", "Report", new { operationId = id });
        }
    }
}